using System;
using System.Threading.Tasks;
using InterviewTraining.Application.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace InterviewTraining.Infrastructure.SignalR;

/// <summary>
/// Реализация сервиса уведомлений через SignalR
/// </summary>
public class InterviewNotificationService : IInterviewNotificationService
{
    private readonly IHubContext<ChatHub> _chatHubContext;
    private readonly IHubContext<InterviewHub> _interviewHubContext;
    private readonly ILogger<InterviewNotificationService> _logger;

    public InterviewNotificationService(
        IHubContext<ChatHub> chatHubContext,
        IHubContext<InterviewHub> interviewHubContext,
        ILogger<InterviewNotificationService> logger)
    {
        _chatHubContext = chatHubContext;
        _interviewHubContext = interviewHubContext;
        _logger = logger;
    }

    /// <summary>
    /// Отправить уведомление о новом сообщении в чате
    /// </summary>
    public async Task NotifyChatMessageCreatedAsync(ChatMessageNotificationDto message)
    {
        try
        {
            var groupName = $"interview_{message.InterviewId}_chat";
            await _chatHubContext.Clients.Group(groupName)
                .SendAsync(ChatHub.MessageCreatedMethod, message);

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
    public async Task NotifyChatMessageUpdatedAsync(ChatMessageNotificationDto message)
    {
        try
        {
            var groupName = $"interview_{message.InterviewId}_chat";
            await _chatHubContext.Clients.Group(groupName)
                .SendAsync(ChatHub.MessageUpdatedMethod, message);

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
    public async Task NotifyInterviewVersionChangedAsync(InterviewVersionNotificationDto notification)
    {
        try
        {
            var groupName = $"interview_{notification.InterviewId}";
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
