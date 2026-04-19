using System;
using System.Collections.Generic;

namespace InterviewTraining.Domain;

/// <summary>
/// Навык
/// </summary>
public class Skill : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// Подтверждён навык или нет
    /// </summary>
    public bool IsConfirmed { get; set; }

    /// <summary>
    /// Наименование навыка
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Идентификатор группы (опционально)
    /// </summary>
    public Guid? GroupId { get; set; }

    /// <summary>
    /// Навигационное свойство к группе
    /// </summary>
    public SkillGroup Group { get; set; }

    ///<summary>
    /// Коллекция тэгов навыка
    /// </summary>
    public ICollection<SkillTag> Tags { get; set; }
}
