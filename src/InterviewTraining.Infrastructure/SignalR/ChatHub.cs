using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.SignalR;

/// <summary>
/// SignalR хаб для уведомлений о сообщениях чата
/// </summary>
[Authorize]
public class ChatHub : Hub
{
    /// <summary>
    /// Присоединиться к группе чата интервью
    /// </summary>
    public async Task JoinInterviewChat(Guid interviewId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"interview_{interviewId}_chat");
    }

    /// <summary>
    /// Покинуть группу чата интервью
    /// </summary>
    public async Task LeaveInterviewChat(Guid interviewId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"interview_{interviewId}_chat");
    }

    /// <summary>
    /// Имя метода для уведомления о новом сообщении
    /// </summary>
    public const string MessageCreatedMethod = "ChatMessageCreated";

    /// <summary>
    /// Имя метода для уведомления об обновлении сообщения
    /// </summary>
    public const string MessageUpdatedMethod = "ChatMessageUpdated";
}
