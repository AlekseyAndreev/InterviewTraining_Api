namespace InterviewTraining.Domain;

/// <summary>
/// Тип доступности пользователя для собеседований
/// </summary>
public enum AvailabilityType
{
    /// <summary>
    /// Доступен всегда
    /// </summary>
    AlwaysAvailable = 0,
    
    /// <summary>
    /// Каждый [DayOfWeek] весь день
    /// </summary>
    WeeklyFullDay = 1,
    
    /// <summary>
    /// Каждый [DayOfWeek] в указанное время [StartTime] - [EndTime]
    /// </summary>
    WeeklyWithTime = 2,
    
    /// <summary>
    /// Конкретная дата [SpecificDate] в указанное время [StartTime] - [EndTime]
    /// </summary>
    SpecificDateTime = 3
}
