using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.CreateInterview.V10;

/// <summary>
/// Обработчик запроса на создание собеседования
/// </summary>
public class CreateInterviewHandler(IInterviewService interviewService) : IMediatorHandler<CreateInterviewRequest, CreateInterviewResponse>
{
    public Task<CreateInterviewResponse> HandleAsync(CreateInterviewRequest request, CancellationToken cancellationToken) =>
        interviewService.CreateInterviewAsync(request, cancellationToken);
}
