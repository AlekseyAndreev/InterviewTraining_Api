using System.Collections.Generic;

namespace InterviewTraining.Application.GetUserNotifications.V10;

public class GetUserNotificationsResponse
{
    public IReadOnlyCollection<UserNotificationDto> Notifications { get; set; }
}
