using InterviewTraining.Application.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.SignalR;

/// <summary>
/// Реализация сервиса уведомлений через SignalR
/// </summary>
public class InterviewNotificationProvider : IInterviewNotificationProvider
{
    private readonly IHubContext<InterviewChatHub> _chatHubContext;
    private readonly IHubContext<InterviewHub> _interviewHubContext;
    private readonly ILogger<InterviewNotificationProvider> _logger;

    public InterviewNotificationProvider(
        IHubContext<InterviewChatHub> chatHubContext,
        IHubContext<InterviewHub> interviewHubContext,
        ILogger<InterviewNotificationProvider> logger)
    {
        _chatHubContext = chatHubContext;
        _interviewHubContext = interviewHubContext;
        _logger = logger;
    }

    /// <summary>
    /// Отправить уведомление о новом сообщении в чате собеседования
    /// </summary>
    public async Task NotifyChatMessageCreatedAsync(InterviewChatMessageNotificationDto message)
    {
        try
        {
            var groupName = InterviewChatHub.GetGroupName(message.InterviewId);
            await _chatHubContext.Clients.Group(groupName)
                .SendAsync(InterviewChatHub.MessageCreatedMethod, message);

            _logger.LogDebug("Отправлено уведомление о создании сообщения {MessageId} в интервью {InterviewId}",
                message.Id, message.InterviewId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при отправке уведомления о создании сообщения {MessageId}", message.Id);
        }
    }

    /// <summary>
    /// Отправить уведомление об обновлении сообщения в чате
    /// </summary>
    public async Task NotifyChatMessageUpdatedAsync(InterviewChatMessageNotificationDto message)
    {
        try
        {
            var groupName = InterviewChatHub.GetGroupName(message.InterviewId);
            await _chatHubContext.Clients.Group(groupName)
                .SendAsync(InterviewChatHub.MessageUpdatedMethod, message);

            _logger.LogDebug("Отправлено уведомление об обновлении сообщения {MessageId} в интервью {InterviewId}",
                message.Id, message.InterviewId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при отправке уведомления об обновлении сообщения {MessageId}", message.Id);
        }
    }

    /// <summary>
    /// Отправить уведомление об изменении версии интервью
    /// </summary>
    public async Task NotifyInterviewVersionChangedAsync(InterviewVersionChangedNotificationDto notification)
    {
        try
        {
            var groupName = InterviewHub.GetGroupName(notification.InterviewId);
            await _interviewHubContext.Clients.Group(groupName)
                .SendAsync(InterviewHub.VersionChangedMethod, notification);

            _logger.LogDebug("Отправлено уведомление об изменении версии интервью {InterviewId}, тип: {ChangeType}",
                notification.InterviewId, notification.ChangeType);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при отправке уведомления об изменении версии интервью {InterviewId}",
                notification.InterviewId);
        }
    }
}
