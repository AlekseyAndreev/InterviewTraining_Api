using System;

namespace InterviewTraining.Application.SignalR;

/// <summary>
/// DTO для уведомления об изменении версии интервью через SignalR
/// </summary>
public class InterviewVersionNotificationDto
{
    /// <summary>
    /// Идентификатор интервью
    /// </summary>
    public Guid InterviewId { get; set; }

    /// <summary>
    /// Идентификатор новой версии
    /// </summary>
    public Guid VersionId { get; set; }

    /// <summary>
    /// Тип изменения
    /// </summary>
    public InterviewChangeType ChangeType { get; set; }

    /// <summary>
    /// Дата и время начала (UTC)
    /// </summary>
    public DateTime StartUtc { get; set; }

    /// <summary>
    /// Дата и время окончания (UTC)
    /// </summary>
    public DateTime? EndUtc { get; set; }

    /// <summary>
    /// Подтверждено кандидатом
    /// </summary>
    public bool CandidateApproved { get; set; }

    /// <summary>
    /// Подтверждено экспертом
    /// </summary>
    public bool ExpertApproved { get; set; }

    /// <summary>
    /// Отменено кандидатом
    /// </summary>
    public bool CandidateCancelled { get; set; }

    /// <summary>
    /// Отменено экспертом
    /// </summary>
    public bool ExpertCancelled { get; set; }

    /// <summary>
    /// Причина отмены
    /// </summary>
    public string CancelReason { get; set; }
}

/// <summary>
/// Тип изменения интервью
/// </summary>
public enum InterviewChangeType
{
    /// <summary>
    /// Подтверждение
    /// </summary>
    Confirmed = 1,

    /// <summary>
    /// Отмена
    /// </summary>
    Cancelled = 2,

    /// <summary>
    /// Перенос времени
    /// </summary>
    Rescheduled = 3
}
