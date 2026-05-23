using System;

namespace InterviewTraining.Domain;

public class UserNotification : BaseDeleteEntity<Guid>
{
    public Guid UserId { get; set; }
    public AdditionalUserInfo User { get; set; }
    public bool IsRead { get; set; }
    public string Text { get; set; }
    public string LinkType { get; set; }
    public string LinkId { get; set; }
}
