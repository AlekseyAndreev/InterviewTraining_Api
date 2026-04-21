using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.DeleteUserNotification.V10;

public class DeleteUserNotificationHandler : IMediatorHandler<DeleteUserNotificationRequest, DeleteUserNotificationResponse>
{
    private readonly IUserNotificationService _notificationService;

    public DeleteUserNotificationHandler(IUserNotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task<DeleteUserNotificationResponse> HandleAsync(DeleteUserNotificationRequest request, CancellationToken cancellationToken)
    {
        await _notificationService.DeleteAsync(request.NotificationId, request.IdentityUserId, cancellationToken);

        return new DeleteUserNotificationResponse { Success = true };
    }
}
