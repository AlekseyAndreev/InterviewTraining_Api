using InterviewTraining.Application.CustomMediatorLogic;
using System;

namespace InterviewTraining.Application.DeleteUserNotification.V10;

public class DeleteUserNotificationRequest : IMediatorRequest<DeleteUserNotificationResponse>
{
    public Guid NotificationId { get; set; }
    public string IdentityUserId { get; set; }
}
