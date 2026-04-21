using InterviewTraining.Application.Interfaces;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AppUserNotificationDto = InterviewTraining.Application.GetUserNotifications.V10.UserNotificationDto;

namespace InterviewTraining.Infrastructure.Services;

public class UserNotificationService : IUserNotificationService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserNotificationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyCollection<AppUserNotificationDto>> GetUserNotificationsAsync(string userId, CancellationToken cancellationToken)
    {
        var notifications = await _unitOfWork.UserNotifications.FindAsync(
            n => n.User.IdentityUserId == userId && !n.IsDeleted);

        return notifications.Select(n => new AppUserNotificationDto
        {
            Id = n.Id,
            IsRead = n.IsRead,
            Text = n.Text,
            CreatedUtc = n.CreatedUtc,
        }).ToArray();
    }
}
