using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.Interfaces;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.Helpers;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AppUserNotificationDto = InterviewTraining.Application.GetUserNotifications.V10.UserNotificationDto;

namespace InterviewTraining.Infrastructure.Services;

public class UserNotificationService(IUnitOfWork unitOfWork, IUserTimeZoneService userTimeZoneService) : IUserNotificationService
{
    public async Task DeleteAsync(Guid notificationId, string identityUserId, CancellationToken cancellationToken)
    {
        var notification = await unitOfWork.UserNotifications.GetByIdWithRelatedAsync(notificationId, cancellationToken);
        if (notification == null || notification.IsDeleted)
        {
            throw new EntityNotFoundException("Уведомление не найдено");
        }

        if (notification.User.IdentityUserId != identityUserId)
        {
            throw new BusinessLogicException("Уведомление вам не принадлежит");
        }

        notification.IsDeleted = true;
        notification.ModifiedUtc = DateTime.UtcNow;
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AppUserNotificationDto>> GetUserNotificationsAsync(string userId, CancellationToken cancellationToken)
    {
        var currentUser = await unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(userId, cancellationToken);
        var notifications = await unitOfWork.UserNotifications.GetAllNotDeletedWithRelatedAsync(userId, cancellationToken);

        var timeZoneCode = await userTimeZoneService.GetTimeZoneCode(currentUser.TimeZoneId);

        return notifications.Select(n => new AppUserNotificationDto
        {
            Id = n.Id,
            IsRead = n.IsRead,
            Text = n.Text,
            Created = DateTimeHelper.ConvertUtcToUserTimeZone(n.CreatedUtc, timeZoneCode),
        }).ToArray();
    }

    public async Task MarkAsReadAsync(Guid notificationId, bool isRead, string identityUserId, CancellationToken cancellationToken)
    {
        var notification = await unitOfWork.UserNotifications.GetByIdWithRelatedAsync(notificationId, cancellationToken);
        if (notification == null || notification.IsDeleted)
        {
            throw new EntityNotFoundException("Уведомление не найдено");
        }

        if(notification.User.IdentityUserId != identityUserId)
        {
            throw new BusinessLogicException("Уведомление вам не принадлежит");
        }

        notification.IsRead = isRead;
        notification.ModifiedUtc = DateTime.UtcNow;
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
