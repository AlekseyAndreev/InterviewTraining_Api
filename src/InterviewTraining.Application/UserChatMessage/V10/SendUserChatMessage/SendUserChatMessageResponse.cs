using System;

namespace InterviewTraining.Application.UserChatMessage.V10.SendUserChatMessage;

///<summary>
/// Response after sending a message
///</summary>
public class SendUserChatMessageResponse
{
    ///<summary>
    /// Message id
    ///</summary>
    public Guid MessageId { get; set; }

    ///<summary>
    /// Created UTC timestamp
    ///</summary>
    public DateTime CreatedUtc { get; set; }
}
