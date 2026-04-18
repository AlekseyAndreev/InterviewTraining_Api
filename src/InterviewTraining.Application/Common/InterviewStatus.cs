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
    /// Время начала собеседования прошло, кандидат не подтвердил время
    /// </summary>
    public const string TimeExpiredCandidateDidNotApprove = "TimeExpiredCandidateDidNotApprove";

    /// <summary>
    /// Время начала собеседования прошло, эксперт не подтвердил время
    /// </summary>
    public const string TimeExpiredExpertDidNotApprove = "TimeExpiredExpertDidNotApprove";

    /// <summary>
    /// Время начала собеседования прошло, эксперт не подтвердил время, кандидат не подтвердил время
    /// </summary>
    public const string TimeExpiredBothDidNotApprove = "TimeExpiredBothDidNotApprove";

    /// <summary>
    /// Время начала собеседования прошло, эксперт подтвердил время, кандидат подтвердил время, админ не подтвердил
    /// </summary>
    public const string TimeExpiredBothApprovedAdminDidNotApprove = "TimeExpiredBothApprovedAdminDidNotApprove";

    /// <summary>
    /// Подтверждено кандидатом
    /// </summary>
    public const string ConfirmedByCandidate = "ConfirmedByCandidate";

    /// <summary>
    /// Подтверждено экспертом
    /// </summary>
    public const string ConfirmedByExpert = "ConfirmedByExpert";

    /// <summary>
    /// Подтверждено кандидадом, подтверждено экспертом, не подтверждено админом(ссылка не существует, нет оплаты от кандидата, время не наступило)
    /// </summary>
    public const string ConfirmedBothAdminNotApproved = "ConfirmedBothAdminNotApproved";

    /// <summary>
    /// Подтверждено кандидадом, подтверждено экспертом, подтверждено админом (ссылка существует, кандидат оплатил, время не наступило)
    /// </summary>
    public const string ConfirmedBothAdminApprovedTimeDidNotStart = "ConfirmedBothAdminApprovedTimeDidNotStart";

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

    /// <summary>
    /// Удалено экспертом
    /// </summary>
    public const string DeletedByExpert = "DeletedByExpert";

    /// <summary>
    /// Удалено кандидатом
    /// </summary>
    public const string DeletedByCandidate = "DeletedByCandidate";

    /// <summary>
    /// Удалено кандидатом и экспертом одновременно
    /// </summary>
    public const string DeletedByCandidateAndExpert = "DeletedByCandidateAndExpert";
}
