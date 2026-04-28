using System;

namespace InterviewTraining.Domain;

///<summary>
/// User chat message
///</summary>
public class UserChatMessage : BaseEntity<Guid>
{
    ///<summary>
    /// Sender user identifier
    ///</summary>
    public Guid SenderUserId { get; set; }

    ///<summary>
    /// Sender user info
    ///</summary>
    public AdditionalUserInfo SenderUser { get; set; }

    ///<summary>
    /// Receiver user identifier
    ///</summary>
    public Guid ReceiverUserId { get; set; }

    ///<summary>
    /// Receiver user info
    ///</summary>
    public AdditionalUserInfo ReceiverUser { get; set; }

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
    /// Is message deleted
    ///</summary>
    public bool IsDeleted { get; set; }
}
