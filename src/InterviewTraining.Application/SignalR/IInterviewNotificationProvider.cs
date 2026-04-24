using System.Threading.Tasks;

namespace InterviewTraining.Application.SignalR;

/// <summary>
/// Интерфейс сервиса для отправки уведомлений через SignalR
/// </summary>
public interface IInterviewNotificationProvider
{
    /// <summary>
    /// Отправить уведомление о новом сообщении в чате
    /// </summary>
    Task NotifyChatMessageCreatedAsync(InterviewChatMessageNotificationDto message);

    ///<summary>
    /// Отправить уведомление об обновлении сообщения в чате
    /// </summary>
    Task NotifyChatMessageUpdatedAsync(InterviewChatMessageNotificationDto message);

    /// <summary>
    /// Отправить уведомление об изменении версии интервью
    /// </summary>
    Task NotifyInterviewVersionChangedAsync(InterviewVersionChangedNotificationDto notification);
}
