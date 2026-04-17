using System;
using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.CreateInterview.V10;

/// <summary>
/// Запрос на создание собеседования
/// </summary>
public class CreateInterviewRequest : IMediatorRequest<CreateInterviewResponse>
{
    /// <summary>
    /// Идентификатор пользователя из токена (кандидат)
    /// </summary>
    public string CandidateId { get; set; }

    /// <summary>
    /// Идентификатор эксперта
    /// </summary>
    public string ExpertId { get; set; }

    /// <summary>
    /// Дата собеседования
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Время собеседования
    /// </summary>
    public TimeOnly Time { get; set; }

    /// <summary>
    /// Комментарий от кандидата
    /// </summary>
    public string Notes { get; set; }

    /// <summary>
    /// Язык для собеседования
    /// </summary>
    public Guid? InterviewLanguageId { get; set; }
}
