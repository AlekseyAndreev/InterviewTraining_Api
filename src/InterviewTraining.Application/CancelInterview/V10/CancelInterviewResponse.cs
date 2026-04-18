using System;

namespace InterviewTraining.Application.CancelInterview.V10;

/// <summary>
/// Ответ на отмену собеседования
/// </summary>
public class CancelInterviewResponse
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
