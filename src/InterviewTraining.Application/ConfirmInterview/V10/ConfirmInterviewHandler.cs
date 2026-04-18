using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.ConfirmInterview.V10;

/// <summary>
/// Обработчик запроса на подтверждение собеседования
/// </summary>
public class ConfirmInterviewHandler(IInterviewService interviewService) : IMediatorHandler<ConfirmInterviewRequest, ConfirmInterviewResponse>
{
    public Task<ConfirmInterviewResponse> HandleAsync(ConfirmInterviewRequest request, CancellationToken cancellationToken) =>
        interviewService.ConfirmInterviewAsync(request, cancellationToken);
}
