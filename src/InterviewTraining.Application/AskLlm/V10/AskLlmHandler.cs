using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;

namespace InterviewTraining.Application.AskLlm.V10;

/// <summary>
/// Обработчик запроса к LLM
/// </summary>
public class AskLlmHandler(ILlmService llmService) : IMediatorHandler<AskLlmRequest, AskLlmResponse>
{
    public async Task<AskLlmResponse> HandleAsync(AskLlmRequest request, CancellationToken cancellationToken)
    {
        var answerWithSystem = await llmService.GetCompletionAsync(request.InterviewType, request.UserText, cancellationToken);
        return new AskLlmResponse { Answer = answerWithSystem };
    }
}