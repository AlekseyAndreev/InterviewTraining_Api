using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.UpdateUserTimeZone.V10;

public class UpdateUserTimeZoneHandler(IUserService userService) : IMediatorHandler<UpdateUserTimeZoneRequest, UpdateUserTimeZoneResponse>
{
    public async Task<UpdateUserTimeZoneResponse> HandleAsync(UpdateUserTimeZoneRequest request, CancellationToken cancellationToken)
    {
        return await userService.UpdateUserTimeZoneAsync(request, cancellationToken);
    }
}
