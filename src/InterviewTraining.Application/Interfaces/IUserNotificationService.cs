using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.Interfaces;

public interface IUserNotificationService
{
    Task<IReadOnlyCollection<GetUserNotifications.V10.UserNotificationDto>> GetUserNotificationsAsync(string userId, CancellationToken cancellationToken);
    Task MarkAsReadAsync(Guid notificationId, bool isRead, string identityUserId, CancellationToken cancellationToken);
    Task DeleteAsync(Guid notificationId, string identityUserId, CancellationToken cancellationToken);
}
