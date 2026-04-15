using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.DatabaseContext;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterviewTraining.Infrastructure.Repositories;

/// <summary>
/// Репозиторий для работы с доступным временем пользователя
/// </summary>
public class UserAvailableTimeRepository : Repository<UserAvailableTime, Guid>, IUserAvailableTimeRepository
{
    public UserAvailableTimeRepository(InterviewContext context) : base(context)
    {
    }

    /// <summary>
    /// Получить все записи доступного времени пользователя
    /// </summary>
    public async Task<IEnumerable<UserAvailableTime>> GetByUserIdAsync(Guid userId)
    {
        return await DbSet
            .Where(x => x.UserId == userId)
            .OrderBy(x => x.AvailabilityType)
            .ThenBy(x => x.DayOfWeek)
            .ThenBy(x => x.SpecificDate)
            .ToListAsync();
    }

    /// <summary>
    /// Получить активные (не удаленные) записи доступного времени пользователя
    /// </summary>
    public async Task<IEnumerable<UserAvailableTime>> GetActiveByUserIdAsync(Guid userId)
    {
        return await DbSet
            .Where(x => x.UserId == userId && !x.IsDeleted)
            .OrderBy(x => x.AvailabilityType)
            .ThenBy(x => x.DayOfWeek)
            .ThenBy(x => x.SpecificDate)
            .ToListAsync();
    }
}
