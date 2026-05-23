using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.Interfaces;
using InterviewTraining.Application.SignalR;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.Helpers;
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
    private static InterviewVersionChangedBy GetChangedBy(bool isCandidate, bool isExpert, bool isAdmin)
    {
        if (isCandidate)
        {
            return InterviewVersionChangedBy.Candidate;
        }

        if (isExpert)
        {
            return InterviewVersionChangedBy.Expert;
        }

        if (isAdmin)
        {
            return InterviewVersionChangedBy.Expert;
        }

        return InterviewVersionChangedBy.Unknown;
    }

    /// <summary>
    /// Вычисление статуса интервью на основе данных версии
    /// </summary>
    private InterviewVersionState CalculateStatusWithCheck(Interview interview, InterviewVersion version)
    {
        var calculatedStatus = InterviewHelper.CalculateStatus(interview, version);
        var currentStatus = version.State;

        if (calculatedStatus != currentStatus)
        {
            _logger.LogError("Вычисленный статус {CalculatedStatus} и текущий статус {CurrentStatus} не совпадают", calculatedStatus, calculatedStatus);
        }

        return calculatedStatus;
    }

    private async Task<(bool isCandidate, bool isExpert, bool isAdminCurrentUser, Interview interview, InterviewVersion activeVersion, AdditionalUserInfo currentUser)> GetBaseToChangeInterviewAsync(string currentUserId, Guid interviewId, string actionName, bool isAdmin, CancellationToken cancellationToken)
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

        var isAdminCurrentUser = isAdmin && currentUser.IsAdmin;

        if (!isCandidate && !isExpert && !isAdminCurrentUser)
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

        return (isCandidate, isExpert, isAdminCurrentUser, interview, activeVersion, currentUser);
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

        await _unitOfWork.UserNotifications.AddAsync(CreateNotification(interview, chatMessageText, interview.ExpertId), cancellationToken);
        await _unitOfWork.UserNotifications.AddAsync(CreateNotification(interview, chatMessageText, interview.CandidateId), cancellationToken);
        await _unitOfWork.SaveChangesAsync();
    }
}
