using System;
using System.Collections.Generic;

namespace InterviewTraining.Application.GetMyInterviews.V10;

/// <summary>
/// Ответ со списком интервью пользователя
/// </summary>
public class GetMyInterviewsResponse
{
    /// <summary>
    /// Список интервью
    /// </summary>
    public List<InterviewDto> Interviews { get; set; } = new();
}

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
