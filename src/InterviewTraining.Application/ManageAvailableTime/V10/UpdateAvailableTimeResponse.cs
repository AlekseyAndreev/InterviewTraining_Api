using System;

namespace InterviewTraining.Application.ManageAvailableTime.V10;

/// <summary>
/// Ответ на обновление записи доступного времени
/// </summary>
public class UpdateAvailableTimeResponse
{
    /// <summary>
    /// Идентификатор обновленной записи
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Признак успешного выполнения
    /// </summary>
    public bool Success { get; set; }
}
