using System;

namespace InterviewTraining.Domain;

/// <summary>
/// Связь пользователя с навыком
/// </summary>
public class UserSkill : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Идентификатор навыка
    /// </summary>
    public Guid SkillId { get; set; }

    /// <summary>
    /// Навигационное свойство к пользователю
    /// </summary>
    public AdditionalUserInfo User { get; set; }

    /// <summary>
    /// Навигационное свойство к навыку
    /// </summary>
    public Skill Skill { get; set; }
}
