using InterviewTraining.Application.AllLlmInterviews.V10;
using InterviewTraining.Application.AskLlm.V10;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.Interfaces;

/// <summary>
/// Интерфейс сервиса для работы с LLM
/// </summary>
public interface ILlmService
{
    /// <summary>
    /// Получить ответ от языковой модели с системным промптом
    /// </summary>
    /// <param name="systemPrompt">Системный промпт</param>
    /// <param name="userPrompt">Пользовательский промпт</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Ответ модели</returns>
    Task<string> GetStartAsync(LlmInterviewType interviewType, CancellationToken cancellationToken);

    /// <summary>
    /// Получить ответ от языковой модели с системным промптом
    /// </summary>
    /// <param name="systemPrompt">Системный промпт</param>
    /// <param name="userPrompt">Пользовательский промпт</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Ответ модели</returns>
    Task<string> GetCompletionAsync(LlmInterviewType interviewType, string userPrompt, CancellationToken cancellationToken);

    /// <summary>
    /// Получить все доступные собеседования для LLM
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Ответ модели</returns>
    Task<AllLlmInterviewsResponse[]> GetAvailableInterviewsAsync(CancellationToken cancellationToken);
}