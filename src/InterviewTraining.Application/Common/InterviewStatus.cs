namespace InterviewTraining.Application.Common;

/// <summary>
/// Полные статусы интервью
/// </summary>
public static class InterviewStatus
{
    /// <summary>
    /// Не смогли определить статус
    /// </summary>
    public const string Unknown = "Unknown";

    /// <summary>
    /// Черновик (нет активной версии)
    /// </summary>
    public const string Draft = "Draft";

    /// <summary>
    /// Ожидает подтверждения (никто не подтвердил)
    /// </summary>
    public const string PendingConfirmation = "PendingConfirmation";

    /// <summary>
    /// Не состоялось
    /// </summary>
    public const string DidNotTakePlace = "DidNotTakePlace";

    /// <summary>
    /// Подтверждено кандидатом
    /// </summary>
    public const string ConfirmedByCandidate = "ConfirmedByCandidate";

    /// <summary>
    /// Подтверждено экспертом
    /// </summary>
    public const string ConfirmedByExpert = "ConfirmedByExpert";

    /// <summary>
    /// Подтверждено (оба подтвердили, ссылка не существует, время не наступило)
    /// </summary>
    public const string ConfirmedBoth = "ConfirmedBoth";

    /// <summary>
    /// Подтверждено (оба подтвердили, ссылка существует, время не наступило)
    /// </summary>
    public const string ConfirmedBothLinkCreated = "ConfirmedBothLinkCreated";

    /// <summary>
    /// В процессе проведения
    /// </summary>
    public const string InProgress = "InProgress";

    /// <summary>
    /// Завершено
    /// </summary>
    public const string Completed = "Completed";

    /// <summary>
    /// Отменено кандидатом
    /// </summary>
    public const string CancelledByCandidate = "CancelledByCandidate";

    /// <summary>
    /// Отменено кандидатом и экспертом одновременно
    /// </summary>
    public const string CancelledByCandidateAndExpert = "CancelledByCandidateAndExpert";

    /// <summary>
    /// Отменено экспертом
    /// </summary>
    public const string CancelledByExpert = "CancelledByExpert";
}
