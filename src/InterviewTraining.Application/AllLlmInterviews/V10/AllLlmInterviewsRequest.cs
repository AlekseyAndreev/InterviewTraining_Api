using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.AllLlmInterviews.V10;

/// <summary>
/// Запрос всех поддерживаемых видов собеседований для LLM
/// </summary>
public class AllLlmInterviewsRequest : IMediatorRequest<AllLlmInterviewsResponse[]>
{
}