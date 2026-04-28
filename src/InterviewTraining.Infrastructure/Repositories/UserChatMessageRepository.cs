using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.DatabaseContext;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterviewTraining.Infrastructure.Repositories;

///<summary>
/// Repository for user chat messages
///</summary>
public class UserChatMessageRepository : Repository<UserChatMessage, Guid>, IUserChatMessageRepository
{
    ///<summary>
    /// Constructor
    ///</summary>
    public UserChatMessageRepository(InterviewContext context) : base(context)
    {
    }

    ///<summary>
    /// Get messages between user and administrators
    ///</summary>
    public async Task<IEnumerable<UserChatMessage>> GetMessagesWithAdminsAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(x => x.SenderUser)
            .Include(x => x.ReceiverUser)
            .Where(x =>
                !x.IsDeleted &&
                ((x.SenderUserId == userId) || (x.ReceiverUserId == userId)))
            .OrderBy(x => x.CreatedUtc)
            .ToListAsync(cancellationToken);
    }

    ///<summary>
    /// Get message by id with sender and receiver
    ///</summary>
    public async Task<UserChatMessage> GetByIdWithUsersAsync(Guid messageId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(x => x.SenderUser)
            .Include(x => x.ReceiverUser)
            .FirstOrDefaultAsync(x => x.Id == messageId, cancellationToken);
    }

    ///<summary>
    /// Mark messages as read
    ///</summary>
    public async Task MarkMessagesAsReadAsync(Guid receiverId, Guid senderId, CancellationToken cancellationToken = default)
    {
        var messages = await DbSet
            .Where(x => x.ReceiverUserId == receiverId && x.SenderUserId == senderId && !x.IsRead && !x.IsDeleted)
            .ToListAsync(cancellationToken);

        foreach (var message in messages)
        {
            message.IsRead = true;
        }
    }

    ///<summary>
    /// Soft delete message
    ///</summary>
    public async Task<bool> SoftDeleteAsync(Guid messageId, Guid userId, CancellationToken cancellationToken = default)
    {
        var message = await DbSet
            .FirstOrDefaultAsync(x => x.Id == messageId && x.SenderUserId == userId && !x.IsDeleted, cancellationToken);

        if (message == null)
        {
            return false;
        }

        message.IsDeleted = true;
        message.ModifiedUtc = DateTime.UtcNow;
        return true;
    }
}
