using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InterviewTraining.Domain;

namespace InterviewTraining.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с рейтингами пользователей
/// </summary>
public interface IUserRatingRepository : IRepository<UserRating, Guid>
{
    /// <summary>
    /// Получить рейтинги, поставленные пользователем
    /// </summary>
    Task<IEnumerable<UserRating>> GetByUserFromIdAsync(Guid userFromId);

    /// <summary>
    /// Получить рейтинги, полученные пользователем
    /// </summary>
    Task<IEnumerable<UserRating>> GetByUserToIdAsync(Guid userToId);

    /// <summary>
    /// Получить рейтинг с информацией о пользователях
    /// </summary>
    Task<UserRating> GetWithUsersAsync(Guid id);

    /// <summary>
    /// Проверить, ставил ли пользователь рейтинг другому пользователю
    /// </summary>
    Task<bool> ExistsRatingAsync(Guid userFromId, Guid userToId);

    /// <summary>
    /// Получить средний рейтинг пользователя
    /// </summary>
    Task<double> GetAverageRatingAsync(Guid userToId);

    /// <summary>
    /// Получить количество рейтингов пользователя
    /// </summary>
    Task<int> GetRatingCountAsync(Guid userToId);
}
