using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.RescheduleInterview.V10;

/// <summary>
/// Обработчик запроса на изменение времени собеседования
/// </summary>
public class RescheduleInterviewHandler(IInterviewService interviewService) : IMediatorHandler<RescheduleInterviewRequest, RescheduleInterviewResponse>
{
    public Task<RescheduleInterviewResponse> HandleAsync(RescheduleInterviewRequest request, CancellationToken cancellationToken) =>
        interviewService.RescheduleInterviewAsync(request, cancellationToken);
}
