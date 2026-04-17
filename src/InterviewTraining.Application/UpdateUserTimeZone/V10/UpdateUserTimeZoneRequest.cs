using InterviewTraining.Application.CustomMediatorLogic;
using System;

namespace InterviewTraining.Application.UpdateUserTimeZone.V10;

public class UpdateUserTimeZoneRequest : IMediatorRequest<UpdateUserTimeZoneResponse>
{
    public string IdentityUserId { get; set; }
    public Guid? TimeZoneId { get; set; }
}