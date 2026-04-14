using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Domain;

namespace InterviewTraining.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с группами навыков
/// </summary>
public interface ISkillGroupRepository : IRepository<SkillGroup, Guid>
{
    /// <summary>
    /// Получить группу по наименованию
    /// </summary>
    Task<SkillGroup> GetByNameAsync(string name);

    /// <summary>
    /// Получить группу с навыками
    /// </summary>
    Task<SkillGroup> GetWithSkillsAsync(Guid id);

    /// <summary>
    /// Получить корневые группы (без родительской группы)
    /// </summary>
    Task<IEnumerable<SkillGroup>> GetRootGroupsAsync();

    /// <summary>
    /// Получить дочерние группы
    /// </summary>
    Task<IEnumerable<SkillGroup>> GetChildGroupsAsync(Guid parentGroupId);

    /// <summary>
    /// Получить группу с дочерними группами
    /// </summary>
    Task<SkillGroup> GetWithChildGroupsAsync(Guid id);

    /// <summary>
    /// Получить полную иерархию группы (с навыками и дочерними группами)
    /// </summary>
    Task<SkillGroup> GetFullHierarchyAsync(Guid id);

    /// <summary>
    /// Получить все группы с иерархией
    /// </summary>
    Task<IEnumerable<SkillGroup>> GetAllWithHierarchyAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Получить полное дерево навыков и групп одним запросом
    /// </summary>
    Task<IEnumerable<SkillGroup>> GetFullTreeAsync(CancellationToken cancellationToken);
}
