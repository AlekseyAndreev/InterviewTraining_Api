using System;

namespace InterviewTraining.Application.SignalR;

/// <summary>
/// DTO для уведомления о сообщении чата через SignalR
/// </summary>
public class UserWithAdminChatMessageNotificationDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string Text { get; set; }
    public DateTime CreatedUtc { get; set; }
    public bool IsEdited { get; set; }
    public DateTime? ModifiedUtc { get; set; }
}
