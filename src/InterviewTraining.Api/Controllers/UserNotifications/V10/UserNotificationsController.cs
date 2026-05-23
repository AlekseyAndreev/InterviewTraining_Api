using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.DeleteUserNotification.V10;
using InterviewTraining.Application.GetUserNotifications.V10;
using InterviewTraining.Application.UpdateUserNotification.V10;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Api.Controllers.UserNotifications.V10;

[Route("api/v1/users/me/notifications")]
[ApiController]
public class UserNotificationsController : BaseController<UserNotificationsController>
{
    private readonly ICustomMediator _mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    ///<param name="mediator"></param>
    ///<param name="logger"></param>
    public UserNotificationsController(ICustomMediator mediator,
        ILogger<UserNotificationsController> logger
    )
        : base(logger)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить уведомления текущего пользователя
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<GetUserNotificationsResponse> GetUserNotifications(CancellationToken cancellationToken)
    {
        var request = new GetUserNotificationsRequest();
        request.IdenitityUserId = CurrentUserId;
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Удалить уведомление
    /// </summary>
    [HttpDelete("{notificationId}")]
    [Authorize]
    public async Task<DeleteUserNotificationResponse> DeleteNotification(Guid notificationId, CancellationToken cancellationToken)
    {
        var request = new DeleteUserNotificationRequest
        {
            NotificationId = notificationId,
            IdentityUserId = CurrentUserId,
        };
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Пометить уведомление как прочитанное
    /// </summary>
    [HttpPut("{notificationId}/read")]
    [Authorize]
    public async Task<UpdateUserNotificationResponse> MarkNotificationAsRead(Guid notificationId, CancellationToken cancellationToken)
    {
        var request = new UpdateUserNotificationRequest
        {
            NotificationId = notificationId,
            IsRead = true,
            IdentityUserId = CurrentUserId,
        };
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Пометить уведомление как непрочитанное
    /// </summary>
    [HttpPut("{notificationId}/unread")]
    [Authorize]
    public async Task<UpdateUserNotificationResponse> MarkNotificationAsUnread(Guid notificationId, CancellationToken cancellationToken)
    {
        var request = new UpdateUserNotificationRequest
        {
            NotificationId = notificationId,
            IsRead = false,
            IdentityUserId = CurrentUserId,
        };
        return await _mediator.SendAsync(request, cancellationToken);
    }
}
