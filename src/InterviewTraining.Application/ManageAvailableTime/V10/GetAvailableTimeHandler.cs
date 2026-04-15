using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;

namespace InterviewTraining.Application.ManageAvailableTime.V10;

/// <summary>
/// Обработчик запроса на получение списка доступного времени
/// </summary>
public class GetAvailableTimeHandler(IUserAvailableTimeService userAvailableTimeService) 
    : IMediatorHandler<GetAvailableTimeRequest, GetAvailableTimeResponse>
{
    public async Task<GetAvailableTimeResponse> HandleAsync(GetAvailableTimeRequest request, CancellationToken cancellationToken)
    {
        return await userAvailableTimeService.GetByCurrentUserAsync(request.IdentityUserId, cancellationToken);
    }
}
