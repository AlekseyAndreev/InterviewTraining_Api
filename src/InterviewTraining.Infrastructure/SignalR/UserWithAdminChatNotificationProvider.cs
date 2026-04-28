using InterviewTraining.Application.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.SignalR;

/// <summary>
/// Реализация сервиса уведомлений через SignalR
/// </summary>
public class UserWithAdminChatNotificationProvider : IUserWithAdminChatNotificationProvider
{
    private readonly IHubContext<UserWithAdminChatHub> _hubContext;
    private readonly ILogger<UserWithAdminChatNotificationProvider> _logger;

    public UserWithAdminChatNotificationProvider(
        IHubContext<UserWithAdminChatHub> hubContext,
        ILogger<UserWithAdminChatNotificationProvider> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    /// <summary>
    /// Отправить уведомление о новом сообщении в чате собеседования
    /// </summary>
    public async Task NotifyChatMessageCreatedAsync(UserWithAdminChatMessageNotificationDto message)
    {
        try
        {
            var groupName = UserWithAdminChatHub.GetGroupName(message.UserId);
            await _hubContext.Clients.Group(groupName)
                .SendAsync(UserWithAdminChatHub.MessageCreatedMethod, message);

            _logger.LogDebug("Отправлено уведомление о создании сообщения {MessageId} в чате пользователя {UserId} и админа",
                message.Id, message.UserId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при отправке уведомления о создании сообщения {MessageId}", message.Id);
        }
    }

    /// <summary>
    /// Отправить уведомление об обновлении сообщения в чате
    /// </summary>
    public async Task NotifyChatMessageUpdatedAsync(UserWithAdminChatMessageNotificationDto message)
    {
        try
        {
            var groupName = UserWithAdminChatHub.GetGroupName(message.UserId);
            await _hubContext.Clients.Group(groupName)
                .SendAsync(UserWithAdminChatHub.MessageUpdatedMethod, message);

            _logger.LogDebug("Отправлено уведомление об обновлении сообщения {MessageId} в чате пользователя {UserId} и админа",
                message.Id, message.UserId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при отправке уведомления об обновлении сообщения {MessageId}", message.Id);
        }
    }
}
