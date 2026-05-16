using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;

namespace InterviewTraining.Application.AllLlmInterviews.V10;

/// <summary>
/// Обработчик запроса к LLM
/// </summary>
public class AllLlmInterviewsHandler(ILlmService llmService) : IMediatorHandler<AllLlmInterviewsRequest, AllLlmInterviewsResponse[]>
{
    public async Task<AllLlmInterviewsResponse[]> HandleAsync(AllLlmInterviewsRequest request, CancellationToken cancellationToken)
    {
        var allInterviews = await llmService.GetAvailableInterviewsAsync(cancellationToken);
        return allInterviews;
    }
}