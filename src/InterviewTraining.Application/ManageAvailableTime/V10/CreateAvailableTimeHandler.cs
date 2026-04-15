using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;

namespace InterviewTraining.Application.ManageAvailableTime.V10;

/// <summary>
/// Обработчик запроса на создание записи доступного времени
/// </summary>
public class CreateAvailableTimeHandler(IUserAvailableTimeService userAvailableTimeService) 
    : IMediatorHandler<CreateAvailableTimeRequest, CreateAvailableTimeResponse>
{
    public async Task<CreateAvailableTimeResponse> HandleAsync(CreateAvailableTimeRequest request, CancellationToken cancellationToken)
    {
        return await userAvailableTimeService.CreateAsync(request, cancellationToken);
    }
}
