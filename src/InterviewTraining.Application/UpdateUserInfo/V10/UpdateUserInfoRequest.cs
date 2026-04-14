using System;
using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.UpdateUserInfo.V10;

public class UpdateUserInfoRequest : IMediatorRequest<UpdateUserInfoResponse>
{
    public string IdentityUserId { get; set; }
    public string PhotoUrl { get; set; }
    public byte[] Photo { get; set; }
    public string FullName { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }

    /// <summary>
    /// Идентификатор временной зоны (может быть null)
    /// </summary>
    public Guid? TimeZoneId { get; set; }
}
