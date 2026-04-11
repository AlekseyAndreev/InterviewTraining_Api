using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InterviewTraining.Domain;

namespace InterviewTraining.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с тегами навыков
/// </summary>
public interface ISkillTagRepository : IRepository<SkillTag, Guid>
{
    /// <summary>
    /// Получить тег по наименованию
    /// </summary>
    Task<SkillTag> GetByNameAsync(string name);

    /// <summary>
    /// Получить теги по идентификатору навыка
    /// </summary>
    Task<IEnumerable<SkillTag>> GetBySkillIdAsync(Guid skillId);

    /// <summary>
    /// Получить тег с информацией о навыке
    /// </summary>
    Task<SkillTag> GetWithSkillAsync(Guid id);

    /// <summary>
    /// Поиск тегов по названию
    /// </summary>
    Task<IEnumerable<SkillTag>> SearchAsync(string searchTerm);

    /// <summary>
    /// Удалить все теги навыка
    /// </summary>
    Task DeleteBySkillIdAsync(Guid skillId);
}
