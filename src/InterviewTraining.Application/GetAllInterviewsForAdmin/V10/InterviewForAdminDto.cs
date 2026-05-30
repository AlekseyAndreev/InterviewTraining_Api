using System;

namespace InterviewTraining.Application.GetAllInterviewsForAdmin.V10;

/// <summary>
/// DTO интервью для администратора
/// </summary>
public class InterviewForAdminDto
{
    /// <summary>
    /// Идентификатор интервью
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор эксперта
    /// </summary>
    public string ExpertId { get; set; }

    /// <summary>
    /// Имя эксперта
    /// </summary>
    public string ExpertName { get; set; }

    /// <summary>
    /// Идентификатор кандидата
    /// </summary>
    public string CandidateId { get; set; }

    /// <summary>
    /// Имя кандидата
    /// </summary>
    public string CandidateName { get; set; }

    /// <summary>
    /// Текущий статус интервью
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Описание статуса на русском
    /// </summary>
    public string StatusDescriptionRu { get; set; }

    /// <summary>
    /// Описание статуса на английском
    /// </summary>
    public string StatusDescriptionEn { get; set; }

    /// <summary>
    /// Дата и время интервью в UTC
    /// </summary>
    public DateTime ScheduledAtUtc { get; set; }

    /// <summary>
    /// Признак удалённого интервью
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Признак отмены кандидатом
    /// </summary>
    public bool IsCancelledByCandidate { get; set; }

    /// <summary>
    /// Признак отмены экспертом
    /// </summary>
    public bool IsCancelledByExpert { get; set; }

    /// <summary>
    /// Признак подтверждения кандидатом
    /// </summary>
    public bool IsConfirmedByCandidate { get; set; }

    /// <summary>
    /// Признак подтверждения экспертом
    /// </summary>
    public bool IsConfirmedByExpert { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
