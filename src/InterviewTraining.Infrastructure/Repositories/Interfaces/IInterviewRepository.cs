using InterviewTraining.Domain;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с интервью
/// </summary>
public interface IInterviewRepository : IRepository<Interview, Guid>
{
    /// <summary>
    /// Получить все интервью пользователя (где он кандидат или эксперт)
    /// </summary>
    Task<IEnumerable<Interview>> GetByUserIdAsync(Guid userAdditionalInfoId, CancellationToken cancellationToken);

    /// <summary>
    /// Получить интервью с включением связанных данных
    /// </summary>
    Task<Interview> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получить просроченные версии интервью для обработки шедулером
    /// </summary>
    Task<IReadOnlyCollection<Interview>> GetExpiredVersionsForSchedulerAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Получить все интервью для администратора (включая удалённые)
    /// </summary>
    Task<IEnumerable<Interview>> GetAllForAdminAsync(CancellationToken cancellationToken);
}
