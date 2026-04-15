using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InterviewTraining.Domain;

namespace InterviewTraining.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с доступным временем пользователя
/// </summary>
public interface IUserAvailableTimeRepository : IRepository<UserAvailableTime, Guid>
{
    /// <summary>
    /// Получить все записи доступного времени пользователя
    /// </summary>
    Task<IEnumerable<UserAvailableTime>> GetByUserIdAsync(Guid userId);

    /// <summary>
    /// Получить активные (не удаленные) записи доступного времени пользователя
    /// </summary>
    Task<IEnumerable<UserAvailableTime>> GetActiveByUserIdAsync(Guid userId);
}
