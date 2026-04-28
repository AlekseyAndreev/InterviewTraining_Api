using System;

namespace InterviewTraining.Application.UserChatMessage.V10;

///<summary>
/// User chat message DTO
///</summary>
public class UserChatMessageDto
{
    ///<summary>
    /// Message id
    ///</summary>
    public Guid Id { get; set; }

    ///<summary>
    /// Sender user id
    ///</summary>
    public string SenderUserId { get; set; }

    ///<summary>
    /// Sender user full name
    ///</summary>
    public string SenderFullName { get; set; }

    ///<summary>
    /// Receiver user id
    ///</summary>
    public string ReceiverUserId { get; set; }

    ///<summary>
    /// Receiver user full name
    ///</summary>
    public string ReceiverFullName { get; set; }

    ///<summary>
    /// Message text
    ///</summary>
    public string MessageText { get; set; }

    ///<summary>
    /// Is message edited
    ///</summary>
    public bool IsEdited { get; set; }

    ///<summary>
    /// Is message read
    ///</summary>
    public bool IsRead { get; set; }

    ///<summary>
    /// Created in user time
    ///</summary>
    public DateTime Created { get; set; }
}
