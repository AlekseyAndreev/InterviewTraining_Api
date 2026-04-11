using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InterviewTraining.Domain;

namespace InterviewTraining.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с навыками
/// </summary>
public interface ISkillRepository : IRepository<Skill, Guid>
{
    /// <summary>
    /// Получить навык по наименованию
    /// </summary>
    Task<Skill> GetByNameAsync(string name);

    /// <summary>
    /// Получить навыки по идентификатору группы
    /// </summary>
    Task<IEnumerable<Skill>> GetByGroupIdAsync(Guid groupId);

    /// <summary>
    /// Получить навык с тегами
    /// </summary>
    Task<Skill> GetWithTagsAsync(Guid id);

    /// <summary>
    /// Получить навык с группой
    /// </summary>
    Task<Skill> GetWithGroupAsync(Guid id);

    /// <summary>
    /// Получить навык с тегами и группой
    /// </summary>
    Task<Skill> GetWithDetailsAsync(Guid id);

    /// <summary>
    /// Получить все навыки с тегами и группами
    /// </summary>
    Task<IEnumerable<Skill>> GetAllWithDetailsAsync();

    /// <summary>
    /// Поиск навыков по названию или тегу
    /// </summary>
    Task<IEnumerable<Skill>> SearchAsync(string searchTerm);
}
