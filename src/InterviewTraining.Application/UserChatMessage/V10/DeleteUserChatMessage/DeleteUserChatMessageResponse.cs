using System;

namespace InterviewTraining.Application.UserChatMessage.V10.DeleteUserChatMessage;

///<summary>
/// Response after deleting a message
///</summary>
public class DeleteUserChatMessageResponse
{
    ///<summary>
    /// Success flag
    ///</summary>
    public bool Success { get; set; }

    ///<summary>
    /// Message id
    ///</summary>
    public Guid MessageId { get; set; }
}
