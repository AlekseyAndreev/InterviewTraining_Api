using InterviewTraining.Domain;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Repositories.Interfaces;

public interface IUserNotificationRepository : IRepository<UserNotification, Guid>
{
    Task<IReadOnlyCollection<UserNotification>> GetAllNotDeletedWithRelatedAsync(string identityUserId, CancellationToken cancellationToken);

    Task<UserNotification> GetByIdWithRelatedAsync(Guid id, CancellationToken cancellationToken);
}
