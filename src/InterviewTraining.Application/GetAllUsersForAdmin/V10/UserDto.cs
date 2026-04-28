using System;

namespace InterviewTraining.Application.GetAllUsersForAdmin.V10;

///<summary>
/// User DTO
///</summary>
public class UserDto
{
    ///<summary>
    /// User id
    ///</summary>
    public Guid Id { get; set; }

    ///<summary>
    /// Identity user id
    ///</summary>
    public string IdentityUserId { get; set; }

    ///<summary>
    /// Full name
    ///</summary>
    public string FullName { get; set; }

    ///<summary>
    /// Is expert
    ///</summary>
    public bool IsExpert { get; set; }

    ///<summary>
    /// Is candidate
    ///</summary>
    public bool IsCandidate { get; set; }

    ///<summary>
    /// Is candidate
    ///</summary>
    public bool IsDeleted { get; set; }
}
