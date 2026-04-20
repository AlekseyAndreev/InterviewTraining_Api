namespace InterviewTraining.Application.SignalR;

/// <summary>
/// Тип изменения интервью
/// </summary>
public enum InterviewChangeType
{
    /// <summary>
    /// Не известно
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Подтверждение
    /// </summary>
    Confirmed = 1,

    /// <summary>
    /// Отмена
    /// </summary>
    Cancelled = 2,

    /// <summary>
    /// Перенос времени
    /// </summary>
    Rescheduled = 3,
}
