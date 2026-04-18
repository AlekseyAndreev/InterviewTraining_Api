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
            InterviewStatus.ConfirmedBoth => "Подтверждено обеими сторонами",
            InterviewStatus.ConfirmedBothLinkCreated => "Подтверждено, ссылка на звонок создана",
            InterviewStatus.InProgress => "Собеседование в процессе",
            InterviewStatus.Completed => "Собеседование завершено",
            InterviewStatus.CancelledByCandidate => "Отменено кандидатом",
            InterviewStatus.CancelledByExpert => "Отменено экспертом",
            InterviewStatus.CancelledByCandidateAndExpert => "Отменено обеими сторонами",
            InterviewStatus.DidNotTakePlace => "Не состоялось",
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
            InterviewStatus.ConfirmedBoth => "Confirmed by both parties",
            InterviewStatus.ConfirmedBothLinkCreated => "Confirmed, call link created",
            InterviewStatus.InProgress => "Interview in progress",
            InterviewStatus.Completed => "Interview completed",
            InterviewStatus.CancelledByCandidate => "Cancelled by candidate",
            InterviewStatus.CancelledByExpert => "Cancelled by expert",
            InterviewStatus.CancelledByCandidateAndExpert => "Cancelled by both parties",
            InterviewStatus.DidNotTakePlace => "Did not take place",
            _ => "Unknown status"
        };
    }
}
