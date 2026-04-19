using System;
using System.Collections.Generic;

namespace InterviewTraining.Application.GetInterviewInfo.V10;

/// <summary>
/// Ответ с детальной информацией по собеседованию
/// </summary>
public class GetInterviewInfoResponse
{
    /// <summary>
    /// Идентификатор собеседования
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Текущий статус интервью
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Текущий статус интервью
    /// </summary>
    public string StatusDescriptionRu { get; set; }

    /// <summary>
    /// Текущий статус интервью
    /// </summary>
    public string StatusDescriptionEn { get; set; }

    ///<summary>
    /// Дата и время начала интервью (в часовом поясе пользователя)
    /// </summary>
    public DateTime StartDateTime { get; set; }

    ///<summary>
    /// Дата и время окончания интервью (в часовом поясе пользователя)
    /// </summary>
    public DateTime? EndDateTime { get; set; }

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

    /// <summary>
    /// Информация о кандидате
    /// </summary>
    public InterviewParticipantDto Candidate { get; set; }

    /// <summary>
    /// Информация об эксперте
    /// </summary>
    public InterviewParticipantDto Expert { get; set; }

    /// <summary>
    /// Язык собеседования
    /// </summary>
    public InterviewLanguageDto Language { get; set; }

    /// <summary>
    /// Ссылка на видеозвонок
    /// </summary>
    public string LinkToVideoCall { get; set; }

    /// <summary>
    /// Данные подтверждения кандидата
    /// </summary>
    public ParticipantApprovalDto CandidateApproval { get; set; }

    /// <summary>
    /// Данные подтверждения эксперта
    /// </summary>
    public ParticipantApprovalDto ExpertApproval { get; set; }

    public List<ChatMessageDto> ChatMessages { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedUtc { get; set; }
}