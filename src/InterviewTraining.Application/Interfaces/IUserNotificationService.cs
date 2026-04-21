using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.Interfaces;

public interface IUserNotificationService
{
    Task<IReadOnlyCollection<GetUserNotifications.V10.UserNotificationDto>> GetUserNotificationsAsync(string userId, CancellationToken cancellationToken);
}
