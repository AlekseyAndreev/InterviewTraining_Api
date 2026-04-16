using InterviewTraining.Application.CreateInterview.V10;
using InterviewTraining.Application.GetInterviewInfo.V10;
using InterviewTraining.Application.GetMyInterviews.V10;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.Interfaces;

/// <summary>
/// Интерфейс сервиса для работы с интервью
/// </summary>
public interface IInterviewService
{
    /// <summary>
    /// Получить список интервью текущего пользователя
    /// </summary>
    Task<GetMyInterviewsResponse> GetMyInterviewsAsync(string identityUserId, CancellationToken cancellationToken);

    /// <summary>
    /// Создать новое собеседование
    /// </summary>
    Task<CreateInterviewResponse> CreateInterviewAsync(CreateInterviewRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Получить детальную информацию по собеседованию
    /// </summary>
    /// <remarks>
    /// Доступно только кандидату (кто создал), эксперту (кто проводит) или администратору
    /// </remarks>
    Task<GetInterviewInfoResponse> GetInterviewInfoAsync(GetInterviewInfoRequest request, CancellationToken cancellationToken);
}
