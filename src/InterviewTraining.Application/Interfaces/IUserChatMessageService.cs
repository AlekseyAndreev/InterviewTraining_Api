using InterviewTraining.Application.UserChatMessage.V10;
using InterviewTraining.Application.UserChatMessage.V10.DeleteUserChatMessage;
using InterviewTraining.Application.UserChatMessage.V10.EditUserChatMessage;
using InterviewTraining.Application.UserChatMessage.V10.GetMessagesForExactUserToAdmin;
using InterviewTraining.Application.UserChatMessage.V10.GetUserChatMessagesWithAdmins;
using InterviewTraining.Application.UserChatMessage.V10.MarkUserChatMessageAsRead;
using InterviewTraining.Application.UserChatMessage.V10.SendUserChatMessage;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.Interfaces;

///<summary>
/// Service interface for user chat messages
///</summary>
public interface IUserChatMessageService
{
    ///<summary>
    /// Send message from one user to another
    ///</summary>
    Task<SendUserChatMessageResponse> SendMessageAsync(SendUserChatMessageRequest request, CancellationToken cancellationToken);

    ///<summary>
    /// Edit message
    ///</summary>
    Task<EditUserChatMessageResponse> EditMessageAsync(EditUserChatMessageRequest request, CancellationToken cancellationToken);

    ///<summary>
    /// Delete message (soft delete, only own messages)
    ///</summary>
    Task<DeleteUserChatMessageResponse> DeleteMessageAsync(DeleteUserChatMessageRequest request, CancellationToken cancellationToken);

    ///<summary>
    /// Mark message as read
    ///</summary>
    Task<MarkUserChatMessageAsReadResponse> MarkMessageAsReadAsync(MarkUserChatMessageAsReadRequest request, CancellationToken cancellationToken);

    ///<summary>
    /// Get messages between user and administrators
    ///</summary>
    Task<GetUserChatMessagesWithAdminsResponse> GetMessagesWithAdminsAsync(GetUserChatMessagesWithAdminsRequest request, CancellationToken cancellationToken);

    ///<summary>
    /// Get messages between user and administrators
    ///</summary>
    Task<GetMessagesForExactUserToAdminResponse> GetMessagesForExactUserToAdminAsync(GetMessagesForExactUserToAdminRequest request, CancellationToken cancellationToken);
}
