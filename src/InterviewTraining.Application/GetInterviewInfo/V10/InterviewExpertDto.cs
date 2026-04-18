using System;

namespace InterviewTraining.Application.GetInterviewInfo.V10;

/// <summary>
/// DTO эксперта для
/// </summary>
public class InterviewExpertDto : InterviewParticipantDto
{
    /// <summary>
    /// Идентификатор валюты
    /// </summary>
    public Guid? CurrencyId { get; set; }

    /// <summary>
    /// Сумма за интервью
    /// </summary>
    public decimal? InterviewPrice { get; set; }

    /// <summary>
    /// Наименование валюты на русском
    /// </summary>
    public string CurrencyNameRu { get; set; }

    /// <summary>
    /// Наименование валюты на русском
    /// </summary>
    public string CurrencyNameEn { get; set; }
}
