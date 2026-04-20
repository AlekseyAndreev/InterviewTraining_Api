using InterviewTraining.Application.Common;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.Interfaces;
using InterviewTraining.Application.SignalR;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Services;

/// <summary>
/// Сервис для работы с интервью
/// </summary>
public partial class InterviewService : IInterviewService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<InterviewService> _logger;
    private readonly IInterviewNotificationService _notificationService;

    public InterviewService(
        IUnitOfWork unitOfWork,
        ILogger<InterviewService> logger,
        IInterviewNotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _notificationService = notificationService;
    }

    /// <summary>
    /// Конвертация времени пользователя в UTC
    /// </summary>
    private static DateTime ConvertUserTimeToUtc(DateOnly date, TimeOnly time, string timeZoneCode)
    {
        var localDateTime = date.ToDateTime(time);

        if (string.IsNullOrEmpty(timeZoneCode) || timeZoneCode.Equals("UTC", StringComparison.OrdinalIgnoreCase))
        {
            return localDateTime;
        }

        try
        {
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneCode);
            return TimeZoneInfo.ConvertTimeToUtc(localDateTime, timeZoneInfo);
        }
        catch (TimeZoneNotFoundException)
        {
            // Если часовой пояс не найден, считаем что время уже в UTC
            return localDateTime;
        }
        catch (InvalidTimeZoneException)
        {
            return localDateTime;
        }
    }

    /// <summary>
    /// Вычисление статуса интервью на основе данных версии
    /// </summary>
    private static string CalculateStatus(Interview interview, InterviewVersion version)
    {
        if (version == null)
        {
            return InterviewStatus.Draft;
        }

        if (version.Candidate?.IsDeleted == true && version.Expert?.IsDeleted == true)
        {
            return InterviewStatus.DeletedByCandidateAndExpert;
        }

        if (version.Expert?.IsDeleted == true)
        {
            return InterviewStatus.DeletedByExpert;
        }

        if (version.Candidate?.IsDeleted == true)
        {
            return InterviewStatus.DeletedByCandidate;
        }

        if (version.Candidate?.IsCancelled == true && version.Expert?.IsCancelled == true)
        {
            return InterviewStatus.CancelledByCandidateAndExpert;
        }

        if (version.Expert?.IsCancelled == true)
        {
            return InterviewStatus.CancelledByExpert;
        }

        if (version.Candidate?.IsCancelled == true)
        {
            return InterviewStatus.CancelledByCandidate;
        }

        var nowUtc = DateTime.UtcNow;
        var candidateApproved = version.Candidate?.IsApproved ?? false;
        var expertApproved = version.Expert?.IsApproved ?? false;
        var bothApproved = candidateApproved && expertApproved;
        var isAdminApproved = version.IsAdminApproved;

        var isEnd = (version.EndUtc.HasValue && version.EndUtc.Value < nowUtc) || (!version.EndUtc.HasValue && version.StartUtc.AddHours(1) < nowUtc);

        if (bothApproved && isEnd && isAdminApproved)
        {
            return InterviewStatus.Completed;
        }

        var isInProcess = (version.StartUtc <= nowUtc && !version.EndUtc.HasValue && version.StartUtc.AddHours(1) > nowUtc) || (version.StartUtc <= nowUtc && version.EndUtc.HasValue && version.EndUtc.Value > nowUtc);

        if (bothApproved && isInProcess && isAdminApproved)
        {
            return InterviewStatus.InProgress;
        }

        var isStartDateExpired = version.StartUtc <= nowUtc;

        if (!candidateApproved && !expertApproved && isStartDateExpired)
        {
            return InterviewStatus.TimeExpiredBothDidNotApprove;
        }

        if (candidateApproved && !expertApproved && isStartDateExpired)
        {
            return InterviewStatus.TimeExpiredExpertDidNotApprove;
        }

        if (!candidateApproved && expertApproved && isStartDateExpired)
        {
            return InterviewStatus.TimeExpiredCandidateDidNotApprove;
        }

        if (bothApproved && !isAdminApproved && isStartDateExpired)
        {
            return InterviewStatus.TimeExpiredBothApprovedAdminDidNotApprove;
        }

        if (bothApproved && !isAdminApproved && !isStartDateExpired)
        {
            return InterviewStatus.ConfirmedBothAdminNotApproved;
        }

        if (bothApproved && isAdminApproved && !isStartDateExpired)
        {
            return InterviewStatus.ConfirmedBothAdminApprovedTimeDidNotStart;
        }

        if (candidateApproved && !isStartDateExpired)
        {
            return InterviewStatus.ConfirmedByCandidate;
        }

        if (expertApproved && !isStartDateExpired)
        {
            return InterviewStatus.ConfirmedByExpert;
        }

        if (!candidateApproved && !expertApproved && !isStartDateExpired)
        {
            return InterviewStatus.PendingConfirmation;
        }

        return InterviewStatus.Unknown;
    }

    private async Task<string> GetTimeZoneCode(Guid? timeZoneId)
    {
        if (!timeZoneId.HasValue)
        {
            return null;
        }

        var timeZone = await _unitOfWork.TimeZones.GetByIdAsync(timeZoneId.Value);
        return timeZone?.Code;
    }

    private async Task<(bool isCandidate, bool isExpert, Interview interview, InterviewVersion activeVersion, AdditionalUserInfo currentUser)> GetBaseToChangeInterviewAsync(string currentUserId, Guid interviewId, string actionName, CancellationToken cancellationToken)
    {
        var currentUser = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(currentUserId, cancellationToken);
        if (currentUser == null)
        {
            _logger.LogWarning("Не найдена информация по пользователю {UserId}", currentUserId);
            throw new BusinessLogicException("Не найдена информация по текущему пользователю");
        }

        var interview = await _unitOfWork.Interviews.GetWithDetailsAsync(interviewId, cancellationToken);
        if (interview == null)
        {
            _logger.LogWarning("Собеседования с идентификатором {InterviewId} не найдено", interviewId);
            throw new EntityNotFoundException("Собеседование не найдено");
        }

        var isCandidate = interview.CandidateId == currentUser.Id;
        var isExpert = interview.ExpertId == currentUser.Id;

        if (!isCandidate && !isExpert)
        {
            _logger.LogWarning("Пользователь {UserId} не является участником интервью {InterviewId}",
                currentUser.Id, interview.Id);
            throw new BusinessLogicException("Вы не являетесь участником этого собеседования");
        }

        var activeVersion = interview.ActiveInterviewVersion;
        if (activeVersion == null)
        {
            throw new BusinessLogicException("Не задана активная версия для собеседования");
        }

        if (activeVersion.Candidate?.IsDeleted == true || activeVersion.Expert?.IsDeleted == true)
        {
            _logger.LogWarning("Попытка выполнить действие {ActionName} для удалённого собеседования {InterviewId}", actionName, interview.Id);
            throw new BusinessLogicException($"Невозможно выполнить действие {actionName}, так как собеседование удалено");
        }

        if (activeVersion.Candidate?.IsCancelled == true || activeVersion.Expert?.IsCancelled == true)
        {
            _logger.LogWarning("Попытка выполнить действие {ActionName} для отменённого собеседования {InterviewId}", actionName, interview.Id);
            throw new BusinessLogicException($"Невозможно выполнить действие {actionName}, так как собеседование отменено");
        }

        return (isCandidate, isExpert, interview, activeVersion, currentUser);
    }

    private async Task NotifyInterviewChanged(Interview interview, InterviewVersion newVersion, string chatMessageText, CancellationToken cancellationToken)
    {
        await _notificationService.NotifyInterviewVersionChangedAsync(new InterviewVersionChangedNotificationDto
        {
            InterviewId = interview.Id,
            VersionId = newVersion.Id,
            ChangeType = InterviewChangeType.Cancelled,
            StartUtc = newVersion.StartUtc,
            EndUtc = newVersion.EndUtc,
            // CandidateApproved = newVersion.Candidate?.IsApproved ?? false,
            // ExpertApproved = newVersion.Expert?.IsApproved ?? false,
            // CandidateCancelled = newVersion.Candidate?.IsCancelled ?? false,
            // ExpertCancelled = newVersion.Expert?.IsCancelled ?? false,
            // CancelReason = isCandidate ? request.CancelReason : newVersion.Expert?.CancelReason
        });

        await CreateChatMessageInternal(interview.Id, MessageSenderType.System, null, chatMessageText, cancellationToken);
    }

    private static InterviewVersion CopyFrom(Guid interviewId, InterviewVersion activeVersion) =>
        new()
        {
            Id = Guid.NewGuid(),
            InterviewId = interviewId,
            StartUtc = activeVersion.StartUtc,
            EndUtc = activeVersion.EndUtc,
            LinkToVideoCall = activeVersion.LinkToVideoCall,
            LanguageId = activeVersion.LanguageId,
            CreatedUtc = DateTime.UtcNow,
            IsAdminApproved = activeVersion.IsAdminApproved,
            CurrencyId = activeVersion.CurrencyId,
            InterviewPrice = activeVersion.InterviewPrice,
            Candidate = new CandidateInterviewData
            {
                IsApproved = activeVersion.Candidate?.IsApproved ?? false,
                IsPaidByCandidate = activeVersion.Candidate?.IsPaidByCandidate ?? false,
                IsCancelled = activeVersion.Candidate?.IsCancelled ?? false,
                CancelReason = activeVersion.Candidate?.CancelReason,
                IsDeleted = activeVersion.Candidate?.IsDeleted ?? false,
                IsRescheduled = activeVersion.Candidate?.IsRescheduled ?? false,
            },
            Expert = new ExpertInterviewData
            {
                IsApproved = activeVersion.Expert?.IsApproved ?? false,
                IsPaidToExpert = activeVersion.Expert?.IsPaidToExpert ?? false,
                IsCancelled = activeVersion.Expert?.IsCancelled ?? false,
                CancelReason = activeVersion.Expert?.CancelReason,
                IsDeleted = activeVersion.Expert?.IsDeleted ?? false,
                IsRescheduled = activeVersion.Expert?.IsRescheduled ?? false,
            }
        };
}
