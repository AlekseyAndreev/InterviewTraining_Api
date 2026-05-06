namespace InterviewTraining.Domain;

public enum InterviewVersionState
{
    /// <summary>
    /// Статус не определён
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Черновик
    /// </summary>
    Draft = 5,

    /// <summary>
    /// Удалено кандидатом и экспертом
    /// </summary>
    DeletedByCandidateAndExpert = 15,

    /// <summary>
    /// Удалено экспертом
    /// </summary>
    DeletedByExpert = 20,

    /// <summary>
    /// Удалено кандидатом
    /// </summary>
    DeletedByCandidate = 25,

    /// <summary>
    /// Отменено кандидатом и экспертом
    /// </summary>
    CancelledByCandidateAndExpert = 30,

    /// <summary>
    /// Отменено экспертом
    /// </summary>
    CancelledByExpert = 35,

    /// <summary>
    /// Отменено кандидатом
    /// </summary>
    CancelledByCandidate = 40,

    /// <summary>
    /// Создано, никто не заапрувил, ожидает аппрува от эксперта и аппрува от кандидата
    /// </summary>
    PendingConfirmation = 45,

    /// <summary>
    /// Заапрувил эксперт, ожидает аппрува от кандидата
    /// </summary>
    ConfirmedByExpert = 50,

    /// <summary>
    /// Заапрувил кандидат, ожидает аппрува от эксперта
    /// </summary>
    ConfirmedByCandidate = 55,

    /// <summary>
    /// заапрувил кандидат, заапрувил эксперт, заапрувил админ, время собеседования ещё НЕ настало
    /// </summary>
    ConfirmedBothAdminApprovedTimeDidNotStart = 60,

    /// <summary>
    /// Создано, заапрувил кандидат, заапрувил эксперт, админ не поставил аппрув, время собеседования ещё не настало
    /// </summary>
    ConfirmedBothAdminNotApproved = 65,

    /// <summary>
    /// Время начала собеседования прошло, заапрувил кандидат, заапрувил эксперт, админ НЕ поставил аппрув
    /// </summary>
    TimeExpiredBothApprovedAdminDidNotApprove = 70,

    /// <summary>
    /// Время начала собеседования прошло, кандидат НЕ заапрувил, заапрувил эксперт
    /// </summary>
    TimeExpiredCandidateDidNotApprove = 75,

    /// <summary>
    /// Время начала собеседования прошло, эксперт НЕ заапрувил, заапрувил кандидат
    /// </summary>
    TimeExpiredExpertDidNotApprove = 80,

    /// <summary>
    /// Время начала собеседования прошло, эксперт НЕ заапрувил, кандидат НЕ заапрувил
    /// </summary>
    TimeExpiredBothDidNotApprove = 85,

    /// <summary>
    /// Собеседование в процесса
    /// </summary>
    InProgress = 90,

    /// <summary>
    /// Собеседование завершено
    /// </summary>
    Completed = 95,
}
