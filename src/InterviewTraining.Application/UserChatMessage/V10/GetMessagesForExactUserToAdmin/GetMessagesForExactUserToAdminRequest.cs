using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.UserChatMessage.V10.GetMessagesForExactUserToAdmin;

///<summary>
/// Request to get messages between user and administrators
///</summary>
public class GetMessagesForExactUserToAdminRequest : IMediatorRequest<GetMessagesForExactUserToAdminResponse>
{
    ///<summary>
    /// Identity user id
    ///</summary>
    public string CurrentIdentityUserId { get; set; }

    public string ChatWithIdentityUserId { get; set; }

    public bool IsAdmin { get; set; }
}
