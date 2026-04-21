using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.DatabaseContext;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Repositories;

public class UserNotificationRepository : Repository<UserNotification, Guid>, IUserNotificationRepository
{
    public UserNotificationRepository(InterviewContext context) : base(context)
    {
    }

    public async Task<IReadOnlyCollection<UserNotification>> GetAllNotDeletedWithRelatedAsync(string identityUserId, CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(x => x.User.IdentityUserId == identityUserId && !x.IsDeleted)
            .OrderBy(x => x.CreatedUtc)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<UserNotification> GetByIdWithRelatedAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbSet
            .Include(x => x.User)
            .Where(x => x.Id == id && !x.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
