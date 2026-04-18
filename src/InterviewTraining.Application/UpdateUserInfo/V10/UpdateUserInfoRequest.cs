using InterviewTraining.Application.CustomMediatorLogic;
using System;

namespace InterviewTraining.Application.UpdateUserInfo.V10;

public class UpdateUserInfoRequest : IMediatorRequest<UpdateUserInfoResponse>
{
    public string IdentityUserId { get; set; }
    public byte[] Photo { get; set; }
    public string FullName { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public bool IsExpert { get; set; }
    public decimal? InterviewPrice { get; set; }
    public Guid? CurrencyId { get; set; }
}
