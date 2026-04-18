using System;

namespace InterviewTraining.Application.RescheduleInterview.V10;

/// <summary>
/// Ответ на изменение времени собеседования
/// </summary>
public class RescheduleInterviewResponse
{
    /// <summary>
    /// Идентификатор собеседования
    /// </summary>
    public Guid InterviewId { get; set; }

    /// <summary>
    /// Идентификатор новой версии интервью
    /// </summary>
    public Guid NewVersionId { get; set; }

    /// <summary>
    /// Признак успешного выполнения
    /// </summary>
    public bool Success { get; set; }
}
