using System;

namespace InterviewTraining.Application.UserChatMessage.V10.EditUserChatMessage;

///<summary>
/// Response after editing a message
///</summary>
public class EditUserChatMessageResponse
{
    ///<summary>
    /// Message id
    ///</summary>
    public Guid MessageId { get; set; }

    ///<summary>
    /// Updated UTC timestamp
    ///</summary>
    public DateTime UpdatedUtc { get; set; }

    ///<summary>
    /// Is edited flag
    ///</summary>
    public bool IsEdited { get; set; }
}
