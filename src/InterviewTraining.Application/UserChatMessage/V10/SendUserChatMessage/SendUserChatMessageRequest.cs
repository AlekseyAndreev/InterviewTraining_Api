using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.UserChatMessage.V10.SendUserChatMessage;

///<summary>
/// Request to send a message
///</summary>
public class SendUserChatMessageRequest : IMediatorRequest<SendUserChatMessageResponse>
{
    ///<summary>
    /// Identity user id of sender
    ///</summary>
    public string CurrentIdentityUserId { get; set; }

    ///<summary>
    /// Receiver user id
    ///</summary>
    public string ReceiverIdentityUserId { get; set; }

    ///<summary>
    /// Message text
    ///</summary>
    public string MessageText { get; set; }

    ///<summary>
    /// Is sender admin
    ///</summary>
    public bool IsAdmin { get; set; }
}
