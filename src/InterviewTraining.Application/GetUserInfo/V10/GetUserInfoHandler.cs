using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.GetUserInfo.V10;

public class GetUserInfoHandler(IUserService userService) : IMediatorHandler<GetUserInfoRequest, GetUserInfoResponse>
{
    public async Task<GetUserInfoResponse> HandleAsync(GetUserInfoRequest request, CancellationToken cancellationToken)
    {
        return await userService.GetUserInfoAsync(request.IdentityUserId, cancellationToken);
    }
}

