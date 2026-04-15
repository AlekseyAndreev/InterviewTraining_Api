using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;

namespace InterviewTraining.Application.ManageAvailableTime.V10;

/// <summary>
/// Обработчик запроса на удаление записи доступного времени
/// </summary>
public class DeleteAvailableTimeHandler(IUserAvailableTimeService userAvailableTimeService) 
    : IMediatorHandler<DeleteAvailableTimeRequest, DeleteAvailableTimeResponse>
{
    public async Task<DeleteAvailableTimeResponse> HandleAsync(DeleteAvailableTimeRequest request, CancellationToken cancellationToken)
    {
        return await userAvailableTimeService.DeleteAsync(request.IdentityUserId, request.Id, cancellationToken);
    }
}
