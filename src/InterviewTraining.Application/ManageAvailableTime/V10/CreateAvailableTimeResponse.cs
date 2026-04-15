using System;

namespace InterviewTraining.Application.ManageAvailableTime.V10;

///<summary>
/// Ответ на создание записи доступного времени
/// </summary>
public class CreateAvailableTimeResponse
{
    /// <summary>
    /// Идентификатор созданной записи
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Признак успешного выполнения
    /// </summary>
    public bool Success { get; set; }
}
