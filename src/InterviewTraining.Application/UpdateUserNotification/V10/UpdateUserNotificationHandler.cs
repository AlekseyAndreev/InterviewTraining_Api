using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.UpdateUserNotification.V10;

public class UpdateUserNotificationHandler : IMediatorHandler<UpdateUserNotificationRequest, UpdateUserNotificationResponse>
{
    private readonly IUserNotificationService _notificationService;

    public UpdateUserNotificationHandler(IUserNotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task<UpdateUserNotificationResponse> HandleAsync(UpdateUserNotificationRequest request, CancellationToken cancellationToken)
    {
        await _notificationService.MarkAsReadAsync(request.NotificationId, request.IsRead, request.IdentityUserId, cancellationToken);

        return new UpdateUserNotificationResponse { Success = true };
    }
}
