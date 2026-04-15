using System;

namespace InterviewTraining.Application.CreateInterview.V10;

/// <summary>
/// Ответ на создание собеседования
/// </summary>
public class CreateInterviewResponse
{
    /// <summary>
    /// Идентификатор созданного собеседования
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Признак успешного выполнения
    /// </summary>
    public bool Success { get; set; }
}
