using System;

namespace InterviewTraining.Application.ConfirmInterview.V10;

/// <summary>
/// Ответ на подтверждение собеседования
/// </summary>
public class ConfirmInterviewResponse
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
