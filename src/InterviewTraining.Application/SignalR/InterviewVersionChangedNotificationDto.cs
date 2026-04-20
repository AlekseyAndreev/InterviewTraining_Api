using System;

namespace InterviewTraining.Application.SignalR;

/// <summary>
/// DTO для уведомления об изменении версии интервью через SignalR
/// </summary>
public class InterviewVersionChangedNotificationDto
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
}