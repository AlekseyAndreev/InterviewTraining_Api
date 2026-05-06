using System;

namespace InterviewTraining.Application.GetAllUsersForAdmin.V10;

public class UserDto
{
    public Guid Id { get; set; }
    public string IdentityUserId { get; set; }
    public string FullName { get; set; }
    public bool IsExpert { get; set; }
    public bool IsCandidate { get; set; }
    public bool IsDeleted { get; set; }
    public int UnreadMessagesByAdmin { get; set; }
}
