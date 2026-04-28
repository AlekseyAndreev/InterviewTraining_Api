using System.Threading.Tasks;

namespace InterviewTraining.Application.SignalR;

/// <summary>
/// Интерфейс сервиса для отправки уведомлений через SignalR
/// </summary>
public interface IUserWithAdminChatNotificationProvider
{
    /// <summary>
    /// Отправить уведомление о новом сообщении в чате
    /// </summary>
    Task NotifyChatMessageCreatedAsync(UserWithAdminChatMessageNotificationDto message);

    ///<summary>
    /// Отправить уведомление об обновлении сообщения в чате
    /// </summary>
    Task NotifyChatMessageUpdatedAsync(UserWithAdminChatMessageNotificationDto message);
}
