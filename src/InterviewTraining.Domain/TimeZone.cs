using System;

namespace InterviewTraining.Domain;

/// <summary>
/// Часовой пояс (временная зона)
/// </summary>
public class TimeZone : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// Код часового пояса (например, "UTC", "Europe/Moscow", "America/New_York")
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Наименование часового пояса
    /// </summary>
    public string Description { get; set; }
}
