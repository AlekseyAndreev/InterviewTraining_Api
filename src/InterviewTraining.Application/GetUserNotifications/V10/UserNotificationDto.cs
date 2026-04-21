using System;

namespace InterviewTraining.Application.GetUserNotifications.V10;

public class UserNotificationDto
{
    public Guid Id { get; set; }
    public bool IsRead { get; set; }
    public string Text { get; set; }
    public DateTime CreatedUtc { get; set; }
}
