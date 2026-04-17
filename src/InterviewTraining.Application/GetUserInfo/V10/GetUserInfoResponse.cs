using System;
using System.Collections.Generic;

namespace InterviewTraining.Application.GetUserInfo.V10;

public class GetUserInfoResponse
{
    public byte[] Photo { get; set; }
    public string FullName { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }

    /// <summary>
    /// Идентификатор выбранной временной зоны пользователя (может быть null)
    /// </summary>
    public Guid? SelectedTimeZoneId { get; set; }

    /// <summary>
    /// Все доступные временные зоны
    /// </summary>
    public List<TimeZoneDto> TimeZones { get; set; }
}
