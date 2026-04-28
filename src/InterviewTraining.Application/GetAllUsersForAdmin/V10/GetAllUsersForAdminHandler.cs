using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.GetAllUsersForAdmin.V10;

public class GetAllUsersForAdminHandler(IUserService service) : IMediatorHandler<GetAllUsersForAdminRequest, GetAllUsersForAdminResponse>
{
    public async Task<GetAllUsersForAdminResponse> HandleAsync(GetAllUsersForAdminRequest request, CancellationToken cancellationToken)
    {
        return await service.GetAllUsersForAdminAsync(request, cancellationToken);
    }
}
