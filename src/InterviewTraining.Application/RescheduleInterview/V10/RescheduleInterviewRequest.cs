using System;
using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.RescheduleInterview.V10;

/// <summary>
/// Запрос на изменение времени собеседования
/// </summary>
public class RescheduleInterviewRequest : IMediatorRequest<RescheduleInterviewResponse>
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
    /// Новая дата собеседования
    /// </summary>
    public DateOnly NewDate { get; set; }

    /// <summary>
    /// Новое время собеседования
    /// </summary>
    public TimeOnly NewTime { get; set; }
}
