using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.GetUserNotifications.V10;

public class GetUserNotificationsHandler : IMediatorHandler<GetUserNotificationsRequest, GetUserNotificationsResponse>
{
    private readonly IUserNotificationService _notificationService;

    public GetUserNotificationsHandler(IUserNotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task<GetUserNotificationsResponse> HandleAsync(GetUserNotificationsRequest request, CancellationToken cancellationToken)
    {
        var notifications = await _notificationService.GetUserNotificationsAsync(request.IdenitityUserId, cancellationToken);

        return new GetUserNotificationsResponse
        {
            Notifications = notifications
        };
    }
}
