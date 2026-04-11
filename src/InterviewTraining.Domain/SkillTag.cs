using System;

namespace InterviewTraining.Domain;

/// <summary>
/// Тэг навыка (для поиска и альтернативных названий)
/// </summary>
public class SkillTag : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// Идентификатор навыка
    /// </summary>
    public Guid SkillId { get; set; }

    /// <summary>
    /// Навигационное свойство к навыку
    /// </summary>
    public Skill Skill { get; set; }

    /// <summary>
    /// Наименование тэга
    /// </summary>
    public string Name { get; set; }
}
