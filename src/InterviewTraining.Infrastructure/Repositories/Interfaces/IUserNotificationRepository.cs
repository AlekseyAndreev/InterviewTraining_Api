using InterviewTraining.Domain;
using System;

namespace InterviewTraining.Infrastructure.Repositories.Interfaces;

public interface IUserNotificationRepository : IRepository<UserNotification, Guid>
{
}
