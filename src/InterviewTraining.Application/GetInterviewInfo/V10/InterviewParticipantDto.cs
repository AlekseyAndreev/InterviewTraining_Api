using System;

namespace InterviewTraining.Application.GetInterviewInfo.V10;

/// <summary>
/// DTO участника интервью
/// </summary>
public class InterviewParticipantDto
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public string IdentityUserId { get; set; }

    /// <summary>
    /// Полное имя
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Фото пользователя
    /// </summary>
    public byte[] Photo { get; set; }

    /// <summary>
    /// Краткое описание
    /// </summary>
    public string ShortDescription { get; set; }
}
