using InterviewTraining.Application.CustomMediatorLogic;
using System;

namespace InterviewTraining.Application.UserChatMessage.V10.EditUserChatMessage;

///<summary>
/// Request to edit a message
///</summary>
public class EditUserChatMessageRequest : IMediatorRequest<EditUserChatMessageResponse>
{
    ///<summary>
    /// Message id
    ///</summary>
    public Guid MessageId { get; set; }

    ///<summary>
    /// Identity user id of sender
    ///</summary>
    public string IdentityUserId { get; set; }

    ///<summary>
    /// New message text
    ///</summary>
    public string MessageText { get; set; }
}
