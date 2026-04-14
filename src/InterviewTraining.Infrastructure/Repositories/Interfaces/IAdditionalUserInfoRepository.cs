using InterviewTraining.Domain;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с дополнительной информацией пользователей
/// </summary>
public interface IAdditionalUserInfoRepository : IRepository<AdditionalUserInfo, Guid>
{
    /// <summary>
    /// Получить пользователя по IdentityUserId
    /// </summary>
    Task<AdditionalUserInfo> GetByIdentityUserIdAsync(string identityUserId, CancellationToken cancellationToken);

    /// <summary>
    /// Получить всех экспертов
    /// </summary>
    Task<IEnumerable<AdditionalUserInfo>> GetExpertsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Получить всех кандидатов
    /// </summary>
    Task<IEnumerable<AdditionalUserInfo>> GetCandidatesAsync();

    /// <summary>
    /// Получить пользователя с рейтингами
    /// </summary>
    Task<AdditionalUserInfo> GetWithRatingsAsync(Guid id);

    /// <summary>
    /// Проверить существование пользователя по IdentityUserId
    /// </summary>
    Task<bool> ExistsByIdentityUserIdAsync(string identityUserId);

    /// <summary>
    /// Поиск экспертов по описанию
    /// </summary>
    Task<IEnumerable<AdditionalUserInfo>> SearchExpertsAsync(string searchTerm);
}
