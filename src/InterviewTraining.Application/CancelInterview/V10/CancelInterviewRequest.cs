using System;
using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.CancelInterview.V10;

/// <summary>
/// Запрос на отмену собеседования
/// </summary>
public class CancelInterviewRequest : IMediatorRequest<CancelInterviewResponse>
{
    /// <summary>
    /// Идентификатор пользователя из токена
    /// </summary>
    public string IdentityUserId { get; set; }

    /// <summary>
    /// Идентификатор собеседования
    /// </summary>
    public Guid InterviewId { get; set; }

    /// <summary>
    /// Причина отмены (необязательно)
    /// </summary>
    public string CancelReason { get; set; }
}
