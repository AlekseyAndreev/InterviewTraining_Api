using System;
using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.ManageAvailableTime.V10;

/// <summary>
/// Запрос на создание записи доступного времени
/// </summary>
public class CreateAvailableTimeRequest : IMediatorRequest<CreateAvailableTimeResponse>
{
    /// <summary>
    /// Идентификатор пользователя из токена
    /// </summary>
    public string IdentityUserId { get; set; }

    /// <summary>
    /// Тип доступности: 0=AlwaysAvailable, 1=WeeklyFullDay, 2=WeeklyWithTime, 3=SpecificDateTime
    /// </summary>
    public ApplicationAvailabilityType AvailabilityType { get; set; }

    /// <summary>
    /// День недели (0-6), если применимо для WeeklyFullDay и WeeklyWithTime, 0 - Воскресенье, 6 - суббота
    /// </summary>
    public int? DayOfWeek { get; set; }

    /// <summary>
    /// Конкретная дата, если применимо для SpecificDateTime
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
}
