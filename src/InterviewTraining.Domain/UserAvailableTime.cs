using System;

namespace InterviewTraining.Domain;

/// <summary>
/// Доступное время пользователя для проведения собеседований
/// </summary>
public class UserAvailableTime : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// Идентификатор пользователя (эксперта)
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Навигационное свойство к пользователю
    /// </summary>
    public AdditionalUserInfo User { get; set; }

    /// <summary>
    /// Тип доступности
    /// </summary>
    public AvailabilityType AvailabilityType { get; set; }

    /// <summary>
    /// День недели (0-6), если применимо для WeeklyFullDay и WeeklyWithTime
    /// </summary>
    public DayOfWeek? DayOfWeek { get; set; }

    /// <summary>
    /// Конкретная дата, если применимо для SpecificDateTime
    /// </summary>
    public DateOnly? SpecificDate { get; set; }

    /// <summary>
    /// Время начала (в UTC) для WeeklyWithTime и SpecificDateTime
    /// </summary>
    public TimeOnly? StartTime { get; set; }

    /// <summary>
    /// Время окончания (в UTC) для WeeklyWithTime и SpecificDateTime
    /// </summary>
    public TimeOnly? EndTime { get; set; }
}
