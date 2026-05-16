using InterviewTraining.Application.AskLlm.V10;

namespace InterviewTraining.Application.AllLlmInterviews.V10;

/// <summary>
/// Ответ на запрос <see cref="AllLlmInterviewsRequest"/>
/// </summary>
public class AllLlmInterviewsResponse
{
    /// <summary>
    /// Название собеседования
    /// </summary>
    public string InterviewNameRu { get; set; }

    /// <summary>
    /// Название собеседования
    /// </summary>
    public string InterviewNameEn { get; set; }

    /// <summary>
    /// Тип собеседования
    /// </summary>
    public LlmInterviewType InterviewType { get; set; }
}