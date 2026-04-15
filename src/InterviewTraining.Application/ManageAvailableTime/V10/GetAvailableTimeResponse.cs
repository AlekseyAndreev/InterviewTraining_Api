using System.Collections.Generic;

namespace InterviewTraining.Application.ManageAvailableTime.V10;

/// <summary>
/// Ответ на получение списка доступного времени пользователя
/// </summary>
public class GetAvailableTimeResponse
{
    /// <summary>
    /// Список записей доступного времени
    /// </summary>
    public List<AvailableTimeDto> AvailableTimes { get; set; } = new();
}