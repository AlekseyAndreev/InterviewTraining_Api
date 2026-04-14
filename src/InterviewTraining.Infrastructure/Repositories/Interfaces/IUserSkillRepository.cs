using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Domain;

namespace InterviewTraining.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы со связями пользователей и навыков
/// </summary>
public interface IUserSkillRepository : IRepository<UserSkill, Guid>
{
    /// <summary>
    /// Получить все навыки пользователя
    /// </summary>
    Task<IEnumerable<UserSkill>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Получить связь пользователь-навык
    /// </summary>
    Task<UserSkill> GetByUserAndSkillAsync(Guid userId, Guid skillId, CancellationToken cancellationToken);

    /// <summary>
    /// Проверить существование связи
    /// </summary>
    Task<bool> ExistsAsync(Guid userId, Guid skillId, CancellationToken cancellationToken);

    /// <summary>
    /// Удалить все навыки пользователя
    /// </summary>
    Task DeleteByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Получить навыки пользователя с информацией о навыках
    /// </summary>
    Task<IEnumerable<UserSkill>> GetByUserIdWithSkillsAsync(Guid userId, CancellationToken cancellationToken);
}
