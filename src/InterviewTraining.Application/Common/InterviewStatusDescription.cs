namespace InterviewTraining.Application.Common;

/// <summary>
/// Описания статусов интервью
/// </summary>
public static class InterviewStatusDescription
{
    /// <summary>
    /// Получить описание статуса на русском языке
    /// </summary>
    /// <param name="status">Статус интервью</param>
    /// <returns>Описание статуса на русском</returns>
    public static string GetStatusDescriptionRu(string status)
    {
        return status switch
        {
            InterviewStatus.Unknown => "Неизвестный статус",
            InterviewStatus.Draft => "Черновик",
            InterviewStatus.PendingConfirmation => "Ожидает подтверждения участников",
            InterviewStatus.ConfirmedByCandidate => "Подтверждено кандидатом",
            InterviewStatus.ConfirmedByExpert => "Подтверждено экспертом",
            InterviewStatus.ConfirmedBothAdminNotApproved => "Подтверждено обеими сторонами, ожидает подтверждения администратора",
            InterviewStatus.ConfirmedBothAdminApprovedTimeDidNotStart => "Подтверждено, время не настало",
            InterviewStatus.InProgress => "Собеседование в процессе",
            InterviewStatus.Completed => "Собеседование завершено",
            InterviewStatus.CancelledByCandidate => "Отменено кандидатом",
            InterviewStatus.CancelledByExpert => "Отменено экспертом",
            InterviewStatus.CancelledByCandidateAndExpert => "Отменено обеими сторонами",
            InterviewStatus.DeletedByCandidate => "Удалено кандидатом",
            InterviewStatus.DeletedByExpert => "Удалено экспертом",
            InterviewStatus.DeletedByCandidateAndExpert => "Удалено обеими сторонами",
            InterviewStatus.TimeExpiredCandidateDidNotApprove => "Время начала собеседования прошло, кандидат не подтвердил время",
            InterviewStatus.TimeExpiredExpertDidNotApprove => "Время начала собеседования прошло, эксперт не подтвердил время",
            InterviewStatus.TimeExpiredBothDidNotApprove => "Время начала собеседования прошло, эксперт не подтвердил время, кандидат не подтвердил время",
            InterviewStatus.TimeExpiredBothApprovedAdminDidNotApprove => "Время начала собеседования прошло. Администратор не подтвердил собеседование. Пожалуйста, свяжитесь с администратором",
            _ => "Неизвестный статус"
        };
    }

    /// <summary>
    /// Получить описание статуса на английском языке
    /// </summary>
    /// <param name="status">Статус интервью</param>
    /// <returns>Описание статуса на английском</returns>
    public static string GetStatusDescriptionEn(string status)
    {
        return status switch
        {
            InterviewStatus.Unknown => "Unknown status",
            InterviewStatus.Draft => "Draft",
            InterviewStatus.PendingConfirmation => "Pending participant confirmation",
            InterviewStatus.ConfirmedByCandidate => "Confirmed by candidate",
            InterviewStatus.ConfirmedByExpert => "Confirmed by expert",
            InterviewStatus.ConfirmedBothAdminNotApproved => "Confirmed by both parties, waiting administrator's approve",
            InterviewStatus.ConfirmedBothAdminApprovedTimeDidNotStart => "Confirmed, time did not start",
            InterviewStatus.InProgress => "Interview in progress",
            InterviewStatus.Completed => "Interview completed",
            InterviewStatus.CancelledByCandidate => "Cancelled by candidate",
            InterviewStatus.CancelledByExpert => "Cancelled by expert",
            InterviewStatus.CancelledByCandidateAndExpert => "Cancelled by both parties",
            InterviewStatus.DeletedByCandidate => "Deleted by candidate",
            InterviewStatus.DeletedByExpert => "Deleted by expert",
            InterviewStatus.DeletedByCandidateAndExpert => "Deleted by both parties",
            InterviewStatus.TimeExpiredCandidateDidNotApprove => "Time expired. Candidate did not approve",
            InterviewStatus.TimeExpiredExpertDidNotApprove => "Time expired. Expert did not approve",
            InterviewStatus.TimeExpiredBothDidNotApprove => "Time expired. Expert did not approve. Candidate did not approve",
            InterviewStatus.TimeExpiredBothApprovedAdminDidNotApprove => "Time expired. Administrator did not approve. Please contact administrator",
            _ => "Unknown status"
        };
    }
}
