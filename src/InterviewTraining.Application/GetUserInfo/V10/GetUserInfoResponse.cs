using System;
using System.Collections.Generic;

namespace InterviewTraining.Application.GetUserInfo.V10;

public class GetUserInfoResponse
{
    public byte[] Photo { get; set; }
    public string FullName { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public Guid? SelectedTimeZoneId { get; set; }
    public List<TimeZoneDto> TimeZones { get; set; }
}
