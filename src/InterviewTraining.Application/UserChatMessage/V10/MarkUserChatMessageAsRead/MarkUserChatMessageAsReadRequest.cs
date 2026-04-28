using InterviewTraining.Application.CustomMediatorLogic;
using System;

namespace InterviewTraining.Application.UserChatMessage.V10.MarkUserChatMessageAsRead;

///<summary>
/// Request to mark messages as read
///</summary>
public class MarkUserChatMessageAsReadRequest : IMediatorRequest<MarkUserChatMessageAsReadResponse>
{
    ///<summary>
    /// Identity user id
    ///</summary>
    public string IdentityUserId { get; set; }

    ///<summary>
    /// Message id
    ///</summary>
    public Guid MessageId { get; set; }
}
