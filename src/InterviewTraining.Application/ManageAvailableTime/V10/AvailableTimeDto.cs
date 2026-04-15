using System;

namespace InterviewTraining.Application.ManageAvailableTime.V10;

/// <summary>
/// DTO для записи доступного времени
/// </summary>
public class AvailableTimeDto
{
    /// <summary>
    /// Идентификатор записи
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Тип доступности: 0=AlwaysAvailable, 1=WeeklyFullDay, 2=WeeklyWithTime, 3=SpecificDateTime
    /// </summary>
    public ApplicationAvailabilityType AvailabilityType { get; set; }

    /// <summary>
    /// День недели (0-6), если применимо
    /// </summary>
    public int? DayOfWeek { get; set; }

    /// <summary>
    /// Конкретная дата, если применимо
    /// </summary>
    public DateOnly? SpecificDate { get; set; }

    /// <summary>
    /// Время начала (в timezone пользователя)
    /// </summary>
    public TimeOnly? StartTime { get; set; }

    /// <summary>
    /// Время окончания (в timezone пользователя)
    /// </summary>
    public TimeOnly? EndTime { get; set; }

    /// <summary>
    /// Текстовое представление для отображения
    /// </summary>
    public string DisplayTime { get; set; }
}
