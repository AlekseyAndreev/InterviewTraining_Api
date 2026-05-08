using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.ChangeAdminData.V10;

/// <summary>
/// Обработчик запроса на отмену собеседования
/// </summary>
public class ChangeAdminDataHandler(IInterviewService interviewService) : IMediatorHandler<ChangeAdminDataRequest, ChangeAdminDataResponse>
{
    public Task<ChangeAdminDataResponse> HandleAsync(ChangeAdminDataRequest request, CancellationToken cancellationToken) =>
        interviewService.ChangeAdminDataAsync(request, cancellationToken);
}
