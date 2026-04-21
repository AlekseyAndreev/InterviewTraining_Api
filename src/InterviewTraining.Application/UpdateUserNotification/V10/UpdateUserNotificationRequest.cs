using InterviewTraining.Application.CustomMediatorLogic;
using System;

namespace InterviewTraining.Application.UpdateUserNotification.V10;

public class UpdateUserNotificationRequest : IMediatorRequest<UpdateUserNotificationResponse>
{
    public Guid NotificationId { get; set; }
    public bool IsRead { get; set; }
    public string IdentityUserId { get; set; }
}
