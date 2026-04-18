using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.CancelInterview.V10;

/// <summary>
/// Обработчик запроса на отмену собеседования
/// </summary>
public class CancelInterviewHandler(IInterviewService interviewService) : IMediatorHandler<CancelInterviewRequest, CancelInterviewResponse>
{
    public Task<CancelInterviewResponse> HandleAsync(CancelInterviewRequest request, CancellationToken cancellationToken) =>
        interviewService.CancelInterviewAsync(request, cancellationToken);
}
