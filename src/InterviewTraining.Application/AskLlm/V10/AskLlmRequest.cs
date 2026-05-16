using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.AskLlm.V10;

/// <summary>
/// Запрос к LLM
/// </summary>
public class AskLlmRequest : IMediatorRequest<AskLlmResponse>
{
    /// <summary>
    /// Текст запроса
    /// </summary>
    public string UserText { get; set; }

    /// <summary>
    /// Тип собеседования(от этого зависит системный промт)
    /// </summary>
    public LlmInterviewType InterviewType { get; set; }
}