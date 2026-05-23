using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.Interfaces;
using InterviewTraining.Application.SignalR;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.Helpers;
using InterviewTraining.Infrastructure.Providers;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Services;

///<summary>
/// Сервис шедулера для обработки просроченных интервью
///</summary>
public class InterviewSchedulerService : IInterviewSchedulerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInterviewChatMessageProvider _chatMessageProvider;
    private readonly IInterviewNotificationProvider _notificationProvider;
    private readonly ILogger<InterviewSchedulerService> _logger;

    public InterviewSchedulerService(
        IUnitOfWork unitOfWork,
        IInterviewChatMessageProvider chatMessageProvider,
        IInterviewNotificationProvider notificationProvider,
        ILogger<InterviewSchedulerService> logger)
    {
        _unitOfWork = unitOfWork;
        _chatMessageProvider = chatMessageProvider;
        _notificationProvider = notificationProvider;
        _logger = logger;
    }

    ///<summary>
    /// Обработать все просроченные интервью
    ///</summary>
    public async Task ProcessExpiredInterviewsAsync(CancellationToken cancellationToken)
    {
        var expiredInterviews = await _unitOfWork.Interviews
            .GetExpiredVersionsForSchedulerAsync(cancellationToken);

        if (expiredInterviews.Count == 0)
        {
            _logger.LogDebug("Нет просроченных интервью для обработки");
            return;
        }

        _logger.LogInformation("Найдено {Count} просроченных интервью для обработки", expiredInterviews.Count);

        foreach (var interview in expiredInterviews)
        {
            try
            {
                await ProcessSingleInterviewAsync(interview, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка обработки версии {VersionId} интервью {InterviewId}",
                    interview.ActiveInterviewVersionId, interview.Id);
            }
        }
    }

    ///<summary>
    /// Обработать одну просроченную версию интервью
    ///</summary>
    private async Task ProcessSingleInterviewAsync(Interview interview, CancellationToken cancellationToken)
    {
        var version = interview.ActiveInterviewVersion ?? throw new BusinessLogicException("Не задана активная версия интервью");

        var calculatedState = InterviewHelper.CalculateStatus(interview, version);

        if (calculatedState == version.State)
        {
            _logger.LogDebug("Текущее состояние для версии {VersionState} совпадает с вычисленным состоянием {CalculatedState}. Действий не требуется", version.State, calculatedState);
            return;
        }

        var newVersion = InterviewHelper.CopyFrom(interview.Id, version);
        newVersion.State = calculatedState;
        newVersion.ChangedBy = InterviewVersionChangedBy.System;
        await _unitOfWork.InterviewVersions.AddAsync(newVersion, cancellationToken);

        interview.ActiveInterviewVersionId = newVersion.Id;
        _unitOfWork.Interviews.Update(interview);

        var messageText = GenerateMessage(calculatedState);

        if (!string.IsNullOrEmpty(messageText))
        {
            var notifications = CreateNotifications(interview, messageText);
            if (notifications != null && notifications.Any())
            {
                await _unitOfWork.UserNotifications.AddRangeAsync(notifications, cancellationToken);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (!string.IsNullOrEmpty(messageText))
        {
            await _chatMessageProvider.CreateInterviewChatMessage(
                version.InterviewId,
                MessageSenderType.System,
                senderUserId: null,
                messageText,
                cancellationToken);

            await _notificationProvider.NotifyInterviewVersionChangedAsync(new InterviewVersionChangedNotificationDto
            {
                InterviewId = version.InterviewId,
                VersionId = newVersion.Id,
                ChangeType = InterviewChangeType.TimeExpired,
                StartUtc = newVersion.StartUtc,
                EndUtc = newVersion.EndUtc
            });
        }

        _logger.LogInformation(
            "Обработано интервью {InterviewId}, версия {VersionId}, новый статус: {State}", 
            version.InterviewId, version.Id, calculatedState);
    }

    ///<summary>
    /// Получить сообщение в зависимости от вычисленного статуса
    ///</summary>
    private static string GenerateMessage(InterviewVersionState calculatedState) =>
        calculatedState switch
        {
            InterviewVersionState.TimeExpiredBothApprovedAdminDidNotApprove => "Время собеседования истекло. Участники подтвердили участие, но администратор не одобрил. Собеседование не проведено.",
            InterviewVersionState.TimeExpiredBothDidNotApprove => "Время собеседования истекло. Ни один из участников не подтвердил участие. Собеседование не проведено.",
            InterviewVersionState.TimeExpiredCandidateDidNotApprove => "Время собеседования истекло. Кандидат не подтвердил участие. Собеседование не проведено.",
            InterviewVersionState.TimeExpiredExpertDidNotApprove => "Время собеседования истекло. Эксперт не подтвердил участие. Собеседование не проведено.",
            InterviewVersionState.Completed => "Собеседование завершено.",
            InterviewVersionState.InProgress => "Собеседование в процессе.",
            _ => string.Empty,
        };

    ///<summary>
    /// Создать уведомления для кандидата и эксперта
    ///</summary>
    private static IEnumerable<UserNotification> CreateNotifications(Interview interview, string message)
    {
        var notifications = new List<UserNotification>();
        var now = DateTime.UtcNow;

        if (interview.CandidateId != Guid.Empty)
        {
            notifications.Add(new UserNotification
            {
                Id = Guid.NewGuid(),
                UserId = interview.CandidateId,
                Text = message,
                IsRead = false,
                IsDeleted = false,
                CreatedUtc = now,
                ModifiedUtc = null,
                LinkType = UserNotificationLinkTypeConstants.LinkTypeInterview,
                LinkId = interview.Id.ToString()
            });
        }

        if (interview.ExpertId != Guid.Empty)
        {
            notifications.Add(new UserNotification
            {
                Id = Guid.NewGuid(),
                UserId = interview.ExpertId,
                Text = message,
                IsRead = false,
                IsDeleted = false,
                CreatedUtc = now,
                ModifiedUtc = null,
                LinkType = UserNotificationLinkTypeConstants.LinkTypeInterview,
                LinkId = interview.Id.ToString()
            });
        }

        return notifications;
    }
}
