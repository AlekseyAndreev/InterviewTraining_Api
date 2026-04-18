using System;

namespace InterviewTraining.Application.GetInterviewInfo.V10;

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
