using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;

namespace InterviewTraining.Application.ManageAvailableTime.V10;

/// <summary>
/// Обработчик запроса на обновление записи доступного времени
/// </summary>
public class UpdateAvailableTimeHandler(IUserAvailableTimeService userAvailableTimeService) 
    : IMediatorHandler<UpdateAvailableTimeRequest, UpdateAvailableTimeResponse>
{
    public async Task<UpdateAvailableTimeResponse> HandleAsync(UpdateAvailableTimeRequest request, CancellationToken cancellationToken)
    {
        return await userAvailableTimeService.UpdateAsync(request, cancellationToken);
    }
}
