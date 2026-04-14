using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.Interfaces;

/// <summary>
/// Интерфейс сервиса для работы с навыками пользователей
/// </summary>
public interface IUserSkillService
{
    /// <summary>
    /// Добавить навыки текущему пользователю
    /// </summary>
    Task AddSkillsToCurrentUserAsync(string identityUserId, IEnumerable<Guid> skillIds, CancellationToken cancellationToken);

    /// <summary>
    /// Получить навыки текущего пользователя
    /// </summary>
    Task<IEnumerable<Guid>> GetUserSkillIdsAsync(string identityUserId, CancellationToken cancellationToken);
}
