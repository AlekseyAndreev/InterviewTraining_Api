using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.SignalR;

/// <summary>
/// SignalR хаб для уведомлений об изменениях интервью
/// </summary>
[Authorize]
public class InterviewHub : Hub
{
    /// <summary>
    /// Присоединиться к группе уведомлений об интервью
    /// </summary>
    public async Task JoinInterviewGroup(Guid interviewId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"interview_{interviewId}");
    }

    /// <summary>
    /// Покинуть группу уведомлений об интервью
    /// </summary>
    public async Task LeaveInterviewGroup(Guid interviewId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"interview_{interviewId}");
    }

    /// <summary>
    /// Имя метода для уведомления об изменении версии
    /// </summary>
    public const string VersionChangedMethod = "InterviewVersionChanged";
}
