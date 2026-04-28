using System.Collections.Generic;

namespace InterviewTraining.Application.UserChatMessage.V10.GetUserChatMessagesWithAdmins;

///<summary>
/// Response with messages between user and administrators
///</summary>
public class GetUserChatMessagesWithAdminsResponse
{
    ///<summary>
    /// List of messages
    ///</summary>
    public List<UserChatMessageDto> Messages { get; set; } = new();
}