using System.Collections.Generic;

namespace InterviewTraining.Application.UserChatMessage.V10.GetMessagesForExactUserToAdmin;

public class GetMessagesForExactUserToAdminResponse
{
    ///<summary>
    /// List of messages
    ///</summary>
    public List<UserChatMessageDto> Messages { get; set; } = new();
}
