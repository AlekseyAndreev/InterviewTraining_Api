using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.UpdateUserInfo.V10;

public class UpdateUserInfoHandler(IUserService userService) : IMediatorHandler<UpdateUserInfoRequest, UpdateUserInfoResponse>
{
    public async Task<UpdateUserInfoResponse> HandleAsync(UpdateUserInfoRequest request, CancellationToken cancellationToken)
    {
        return await userService.UpdateUserInfoAsync(request, cancellationToken);
    }
}
