using System;

namespace InterviewTraining.Application.GetUserInfo.V10;

/// <summary>
/// DTO для временной зоны
/// </summary>
public class TimeZoneDto
{
    /// <summary>
    /// Идентификатор временной зоны
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Код часового пояса (например, "UTC", "Europe/Moscow")
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Наименование часового пояса
    /// </summary>
    public string Description { get; set; }
}
