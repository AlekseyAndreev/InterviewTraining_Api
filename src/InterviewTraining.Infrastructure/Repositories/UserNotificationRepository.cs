using System;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.DatabaseContext;
using InterviewTraining.Infrastructure.Repositories.Interfaces;

namespace InterviewTraining.Infrastructure.Repositories;

public class UserNotificationRepository : Repository<UserNotification, Guid>, IUserNotificationRepository
{
    public UserNotificationRepository(InterviewContext context) : base(context)
    {
    }
}
