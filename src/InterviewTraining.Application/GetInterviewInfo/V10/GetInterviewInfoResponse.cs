using System;

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

    ///<summary>
    /// Дата и время начала интервью (в часовом поясе пользователя)
    /// </summary>
    public DateTime StartDateTime { get; set; }

    ///<summary>
    /// Дата и время окончания интервью (в часовом поясе пользователя)
    /// </summary>
    public DateTime? EndDateTime { get; set; }

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
    /// Примечания от кандидата
    /// </summary>
    public string Notes { get; set; }

    /// <summary>
    /// Данные подтверждения кандидата
    /// </summary>
    public ParticipantApprovalDto CandidateApproval { get; set; }

    /// <summary>
    /// Данные подтверждения эксперта
    /// </summary>
    public ParticipantApprovalDto ExpertApproval { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedUtc { get; set; }
}

/// <summary>
/// DTO участника интервью
/// </summary>
public class InterviewParticipantDto
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Полное имя
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Фото пользователя
    /// </summary>
    public byte[] Photo { get; set; }

    /// <summary>
    /// Краткое описание
    /// </summary>
    public string ShortDescription { get; set; }
}

/// <summary>
/// DTO языка интервью
/// </summary>
public class InterviewLanguageDto
{
    /// <summary>
    /// Идентификатор языка
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Код языка
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Название на русском
    /// </summary>
    public string NameRu { get; set; }

    /// <summary>
    /// Название на английском
    /// </summary>
    public string NameEn { get; set; }
}

/// <summary>
/// DTO данных подтверждения участника
/// </summary>
public class ParticipantApprovalDto
{
    ///<summary>
    /// Подтверждено ли участие
    /// </summary>
    public bool IsApproved { get; set; }

    /// <summary>
    /// Отменено ли
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Причина отмены
    /// </summary>
    public string CancelReason { get; set; }
}
