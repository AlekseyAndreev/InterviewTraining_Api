using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.GetUserInfo.V10;

public class GetUserInfoRequest : IMediatorRequest<GetUserInfoResponse>
{
    public string IdentityUserId { get; set; }
}