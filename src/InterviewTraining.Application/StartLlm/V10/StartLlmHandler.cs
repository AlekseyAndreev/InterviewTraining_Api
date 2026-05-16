using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;

namespace InterviewTraining.Application.StartLlm.V10;

/// <summary>
/// Обработчик запроса к LLM
/// </summary>
public class StartLlmHandler(ILlmService llmService) : IMediatorHandler<StartLlmRequest, StartLlmResponse>
{
    public async Task<StartLlmResponse> HandleAsync(StartLlmRequest request, CancellationToken cancellationToken)
    {
        var answerWithSystem = await llmService.GetStartAsync(request.InterviewType, cancellationToken);
        return new StartLlmResponse { Answer = answerWithSystem };
    }
}