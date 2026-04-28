using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.SignalR;

/// <summary>
/// SignalR хаб для уведомлений о сообщениях чата для пользователя и админа
/// </summary>
[Authorize]
public class UserWithAdminChatHub : Hub
{
    public static string GetGroupName(string userId)
    {
        return $"user_{userId}_with_admin_chat";
    }

    /// <summary>
    /// Присоединиться к группе чата пользователя и админа
    /// </summary>
    public async Task JoinUserWithAdminChat(string userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, GetGroupName(userId));
    }

    /// <summary>
    /// Покинуть группу чата пользователя и админа
    /// </summary>
    public async Task LeaveUserWithAdminChat(string userId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetGroupName(userId));
    }

    /// <summary>
    /// Имя метода для уведомления о новом сообщении
    /// </summary>
    public const string MessageCreatedMethod = "UserWithAdminChatMessageCreated";

    /// <summary>
    /// Имя метода для уведомления об обновлении сообщения
    /// </summary>
    public const string MessageUpdatedMethod = "UserWithAdminChatMessageUpdated";
}
