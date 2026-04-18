using System;
using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.ConfirmInterview.V10;

/// <summary>
/// Запрос на подтверждение собеседования
/// </summary>
public class ConfirmInterviewRequest : IMediatorRequest<ConfirmInterviewResponse>
{
    /// <summary>
    /// Идентификатор пользователя из токена
    /// </summary>
    public string IdentityUserId { get; set; }

    /// <summary>
    /// Идентификатор собеседования
    /// </summary>
    public Guid InterviewId { get; set; }
}
