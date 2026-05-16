using InterviewTraining.Application.AskLlm.V10;
using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.StartLlm.V10;

/// <summary>
/// Стартовый запрос к LLM
/// </summary>
public class StartLlmRequest : IMediatorRequest<StartLlmResponse>
{
    public LlmInterviewType InterviewType { get; set; }
}