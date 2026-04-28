using InterviewTraining.Application.CustomMediatorLogic;
using System;

namespace InterviewTraining.Application.UserChatMessage.V10.DeleteUserChatMessage;

///<summary>
/// Request to delete a message
///</summary>
public class DeleteUserChatMessageRequest : IMediatorRequest<DeleteUserChatMessageResponse>
{
    ///<summary>
    /// Message id
    ///</summary>
    public Guid MessageId { get; set; }

    ///<summary>
    /// Identity user id of sender (only sender can delete)
    ///</summary>
    public string IdentityUserId { get; set; }
}
