using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.Interfaces;
using InterviewTraining.Application.SignalR;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.Providers;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Services;

/// <summary>
/// Сервис для работы с интервью
/// </summary>
public partial class InterviewService(IUnitOfWork _unitOfWork,
        IInterviewChatMessageProvider interviewChatMessageProvider,
        ILogger<InterviewService> _logger,
        IInterviewNotificationProvider _notificationProvider,
        IUserTimeZoneProvider _userTimeZoneService) : IInterviewService
{
    /// <summary>
    /// Вычисление статуса интервью на основе данных версии
    /// </summary>
    private static InterviewVersionState CalculateStatus(Interview interview, InterviewVersion version)
    {
        if (version == null)
        {
            return InterviewVersionState.Draft;
        }

        if (version.Candidate?.IsDeleted == true && version.Expert?.IsDeleted == true)
        {
            return InterviewVersionState.DeletedByCandidateAndExpert;
        }

        if (version.Expert?.IsDeleted == true)
        {
            return InterviewVersionState.DeletedByExpert;
        }

        if (version.Candidate?.IsDeleted == true)
        {
            return InterviewVersionState.DeletedByCandidate;
        }

        if (version.Candidate?.IsCancelled == true && version.Expert?.IsCancelled == true)
        {
            return InterviewVersionState.CancelledByCandidateAndExpert;
        }

        if (version.Expert?.IsCancelled == true)
        {
            return InterviewVersionState.CancelledByExpert;
        }

        if (version.Candidate?.IsCancelled == true)
        {
            return InterviewVersionState.CancelledByCandidate;
        }

        var nowUtc = DateTime.UtcNow;
        var candidateApproved = version.Candidate?.IsApproved ?? false;
        var expertApproved = version.Expert?.IsApproved ?? false;
        var bothApproved = candidateApproved && expertApproved;
        var isAdminApproved = version.IsAdminApproved;

        var isEnd = (version.EndUtc.HasValue && version.EndUtc.Value < nowUtc) || (!version.EndUtc.HasValue && version.StartUtc.AddHours(1) < nowUtc);

        if (bothApproved && isEnd && isAdminApproved)
        {
            return InterviewVersionState.Completed;
        }

        var isInProcess = (version.StartUtc <= nowUtc && !version.EndUtc.HasValue && version.StartUtc.AddHours(1) > nowUtc) || (version.StartUtc <= nowUtc && version.EndUtc.HasValue && version.EndUtc.Value > nowUtc);

        if (bothApproved && isInProcess && isAdminApproved)
        {
            return InterviewVersionState.InProgress;
        }

        var isStartDateExpired = version.StartUtc <= nowUtc;

        if (!candidateApproved && !expertApproved && isStartDateExpired)
        {
            return InterviewVersionState.TimeExpiredBothDidNotApprove;
        }

        if (candidateApproved && !expertApproved && isStartDateExpired)
        {
            return InterviewVersionState.TimeExpiredExpertDidNotApprove;
        }

        if (!candidateApproved && expertApproved && isStartDateExpired)
        {
            return InterviewVersionState.TimeExpiredCandidateDidNotApprove;
        }

        if (bothApproved && !isAdminApproved && isStartDateExpired)
        {
            return InterviewVersionState.TimeExpiredBothApprovedAdminDidNotApprove;
        }

        if (bothApproved && !isAdminApproved && !isStartDateExpired)
        {
            return InterviewVersionState.ConfirmedBothAdminNotApproved;
        }

        if (bothApproved && isAdminApproved && !isStartDateExpired)
        {
            return InterviewVersionState.ConfirmedBothAdminApprovedTimeDidNotStart;
        }

        if (candidateApproved && !isStartDateExpired)
        {
            return InterviewVersionState.ConfirmedByCandidate;
        }

        if (expertApproved && !isStartDateExpired)
        {
            return InterviewVersionState.ConfirmedByExpert;
        }

        if (!candidateApproved && !expertApproved && !isStartDateExpired)
        {
            return InterviewVersionState.PendingConfirmation;
        }

        return InterviewVersionState.Unknown;
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
            throw new EntityNotFoundException("Interview");
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
        await _notificationProvider.NotifyInterviewVersionChangedAsync(new InterviewVersionChangedNotificationDto
        {
            InterviewId = interview.Id,
            VersionId = newVersion.Id,
            ChangeType = InterviewChangeType.Cancelled,
            StartUtc = newVersion.StartUtc,
            EndUtc = newVersion.EndUtc,
        });

        await interviewChatMessageProvider.CreateInterviewChatMessage(interview.Id, MessageSenderType.System, null, chatMessageText, cancellationToken);
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
