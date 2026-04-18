using System;

namespace InterviewTraining.Domain;

/// <summary>
/// Валюта
/// </summary>
public class Currency : BaseEntity<Guid>
{
    /// <summary>
    /// Код валюты (ISO 4217)
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Название валюты на русском языке
    /// </summary>
    public string NameRu { get; set; }

    /// <summary>
    /// Название валюты на английском языке
    /// </summary>
    public string NameEn { get; set; }
}
