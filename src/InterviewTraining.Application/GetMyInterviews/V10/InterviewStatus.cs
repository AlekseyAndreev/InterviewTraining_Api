namespace InterviewTraining.Application.GetMyInterviews.V10;

/// <summary>
/// Статусы интервью
/// </summary>
public static class InterviewStatus
{
    /// <summary>
    /// Отменено
    /// </summary>
    public const string Cancelled = "Cancelled";

    /// <summary>
    /// Ожидает подтверждения
    /// </summary>
    public const string PendingConfirmation = "PendingConfirmation";

    /// <summary>
    /// Подтверждено
    /// </summary>
    public const string Confirmed = "Confirmed";

    ///<summary>
    /// Завершено
    /// </summary>
    public const string Completed = "Completed";
}
