using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Domain;

namespace InterviewTraining.Infrastructure.Repositories.Interfaces;

///<summary>
/// Repository interface for user chat messages
///</summary>
public interface IUserChatMessageRepository : IRepository<UserChatMessage, Guid>
{
    ///<summary>
    /// Get messages between user and administrators
    ///</summary>
    Task<IEnumerable<UserChatMessage>> GetMessagesWithAdminsAsync(
        Guid userId,
        CancellationToken cancellationToken);

    ///<summary>
    /// Get message by id with sender and receiver
    ///</summary>
    Task<UserChatMessage> GetByIdWithUsersAsync(Guid messageId, CancellationToken cancellationToken = default);

    ///<summary>
    /// Mark messages as read
    ///</summary>
    Task MarkMessagesAsReadAsync(Guid receiverId, Guid senderId, CancellationToken cancellationToken = default);

    ///<summary>
    /// Soft delete message
    ///</summary>
    Task<bool> SoftDeleteAsync(Guid messageId, Guid userId, CancellationToken cancellationToken = default);
}
