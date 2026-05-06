using InterviewTraining.Domain;

namespace InterviewTraining.Infrastructure.Helpers;

/// <summary>
/// Описания статусов интервью
/// </summary>
public static class InterviewStatusDescriptionHelper
{
    /// <summary>
    /// Получить описание статуса на русском языке
    /// </summary>
    /// <param name="status">Статус интервью</param>
    /// <returns>Описание статуса на русском</returns>
    public static string GetStatusDescriptionRu(InterviewVersionState status)
    {
        return status switch
        {
            InterviewVersionState.Unknown => "Неизвестный статус",
            InterviewVersionState.Draft => "Черновик",
            InterviewVersionState.PendingConfirmation => "Ожидает подтверждения участников",
            InterviewVersionState.ConfirmedByCandidate => "Подтверждено кандидатом, ожидает подтверждения экспертом",
            InterviewVersionState.ConfirmedByExpert => "Подтверждено экспертом, ожидает подтверждения кандидатом",
            InterviewVersionState.ConfirmedBothAdminNotApproved => "Подтверждено обеими сторонами, ожидает подтверждения администратора",
            InterviewVersionState.ConfirmedBothAdminApprovedTimeDidNotStart => "Подтверждено, время не настало",
            InterviewVersionState.InProgress => "Собеседование в процессе",
            InterviewVersionState.Completed => "Собеседование завершено",
            InterviewVersionState.CancelledByCandidate => "Отменено кандидатом",
            InterviewVersionState.CancelledByExpert => "Отменено экспертом",
            InterviewVersionState.CancelledByCandidateAndExpert => "Отменено обеими сторонами",
            InterviewVersionState.DeletedByCandidate => "Удалено кандидатом",
            InterviewVersionState.DeletedByExpert => "Удалено экспертом",
            InterviewVersionState.DeletedByCandidateAndExpert => "Удалено обеими сторонами",
            InterviewVersionState.TimeExpiredCandidateDidNotApprove => "Время начала собеседования прошло, кандидат не подтвердил время",
            InterviewVersionState.TimeExpiredExpertDidNotApprove => "Время начала собеседования прошло, эксперт не подтвердил время",
            InterviewVersionState.TimeExpiredBothDidNotApprove => "Время начала собеседования прошло, эксперт не подтвердил время, кандидат не подтвердил время",
            InterviewVersionState.TimeExpiredBothApprovedAdminDidNotApprove => "Время начала собеседования прошло. Администратор не подтвердил собеседование. Пожалуйста, свяжитесь с администратором",
            _ => "Неизвестный статус"
        };
    }

    /// <summary>
    /// Получить описание статуса на английском языке
    /// </summary>
    /// <param name="status">Статус интервью</param>
    /// <returns>Описание статуса на английском</returns>
    public static string GetStatusDescriptionEn(InterviewVersionState status)
    {
        return status switch
        {
            InterviewVersionState.Unknown => "Unknown status",
            InterviewVersionState.Draft => "Draft",
            InterviewVersionState.PendingConfirmation => "Pending participant confirmation",
            InterviewVersionState.ConfirmedByCandidate => "Confirmed by candidate, waiting expert approval",
            InterviewVersionState.ConfirmedByExpert => "Confirmed by expert, waiting candidate approval",
            InterviewVersionState.ConfirmedBothAdminNotApproved => "Confirmed by both parties, waiting administrator's approve",
            InterviewVersionState.ConfirmedBothAdminApprovedTimeDidNotStart => "Confirmed, time did not start",
            InterviewVersionState.InProgress => "Interview in progress",
            InterviewVersionState.Completed => "Interview completed",
            InterviewVersionState.CancelledByCandidate => "Cancelled by candidate",
            InterviewVersionState.CancelledByExpert => "Cancelled by expert",
            InterviewVersionState.CancelledByCandidateAndExpert => "Cancelled by both parties",
            InterviewVersionState.DeletedByCandidate => "Deleted by candidate",
            InterviewVersionState.DeletedByExpert => "Deleted by expert",
            InterviewVersionState.DeletedByCandidateAndExpert => "Deleted by both parties",
            InterviewVersionState.TimeExpiredCandidateDidNotApprove => "Time expired. Candidate did not approve",
            InterviewVersionState.TimeExpiredExpertDidNotApprove => "Time expired. Expert did not approve",
            InterviewVersionState.TimeExpiredBothDidNotApprove => "Time expired. Expert did not approve. Candidate did not approve",
            InterviewVersionState.TimeExpiredBothApprovedAdminDidNotApprove => "Time expired. Administrator did not approve. Please contact administrator",
            _ => "Unknown status"
        };
    }
}
