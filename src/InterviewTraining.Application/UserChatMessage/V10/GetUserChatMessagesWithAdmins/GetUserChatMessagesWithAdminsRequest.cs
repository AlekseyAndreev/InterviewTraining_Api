using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.UserChatMessage.V10.GetUserChatMessagesWithAdmins;

///<summary>
/// Request to get messages between user and administrators
///</summary>
public class GetUserChatMessagesWithAdminsRequest : IMediatorRequest<GetUserChatMessagesWithAdminsResponse>
{
    ///<summary>
    /// Identity user id
    ///</summary>
    public string IdentityUserId { get; set; }
}
