using System;
using System.Collections.Generic;

namespace InterviewTraining.Domain;

/// <summary>
/// Группа навыков
/// </summary>
public class SkillGroup : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// Наименование группы
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Идентификатор родительской группы (опционально)
    /// </summary>
    public Guid? ParentGroupId { get; set; }

    /// <summary>
    /// Навигационное свойство к родительской группе
    /// </summary>
    public SkillGroup ParentGroup { get; set; }

    /// <summary>
    /// Дочерние группы
    /// </summary>
    public ICollection<SkillGroup> ChildGroups { get; set; }

    /// <summary>
    /// Навыки в данной группе
    /// </summary>
    public ICollection<Skill> Skills { get; set; }
}
