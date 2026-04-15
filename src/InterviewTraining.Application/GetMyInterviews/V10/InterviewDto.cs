using System;

namespace InterviewTraining.Application.GetMyInterviews.V10;

/// <summary>
/// DTO интервью
/// </summary>
public class InterviewDto
{
    /// <summary>
    /// Идентификатор интервью
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Текущий статус интервью
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Дата и время интервью в часовом поясе пользователя
    /// </summary>
    public DateTime InterviewDate { get; set; }

    /// <summary>
    /// Имя эксперта
    /// </summary>
    public string ExpertName { get; set; }

    /// <summary>
    /// Имя кандидата
    /// </summary>
    public string CandidateName { get; set; }
}
