using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Интерфейс паттерна Unit of Work для управления транзакциями и репозиториями
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Репозиторий навыков
    /// </summary>
    ISkillRepository Skills { get; }

    /// <summary>
    /// Репозиторий групп навыков
    /// </summary>
    ISkillGroupRepository SkillGroups { get; }

    /// <summary>
    /// Репозиторий тегов навыков
    /// </summary>
    ISkillTagRepository SkillTags { get; }

    /// <summary>
    /// Репозиторий рейтингов пользователей
    /// </summary>
    IUserRatingRepository UserRatings { get; }

    /// <summary>
    /// Репозиторий дополнительной информации пользователей
    /// </summary>
    IAdditionalUserInfoRepository AdditionalUserInfos { get; }

    /// <summary>
    /// Репозиторий часовых поясов
    /// </summary>
    ITimeZoneRepository TimeZones { get; }

    /// <summary>
    /// Репозиторий связей пользователей и навыков
    /// </summary>
    IUserSkillRepository UserSkills { get; }

    /// <summary>
    /// Репозиторий доступного времени пользователей
    /// </summary>
    IUserAvailableTimeRepository UserAvailableTimes { get; }

    /// <summary>
    /// Сохранить все изменения
    /// </summary>
    Task<int> SaveChangesAsync();

    /// <summary>
    /// Сохранить все изменения
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Начать транзакцию
    /// </summary>
    Task BeginTransactionAsync();

    /// <summary>
    /// Зафиксировать транзакцию
    /// </summary>
    Task CommitTransactionAsync();

    /// <summary>
    /// Откатить транзакцию
    /// </summary>
    Task RollbackTransactionAsync();
}
