using InterviewTraining.Application.CancelInterview.V10;
using InterviewTraining.Application.ConfirmInterview.V10;
using InterviewTraining.Application.CreateInterview.V10;
using InterviewTraining.Application.GetInterviewInfo.V10;
using InterviewTraining.Application.GetMyInterviews.V10;
using InterviewTraining.Application.RescheduleInterview.V10;
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

    /// <summary>
    /// Отменить собеседование
    /// </summary>
    /// <remarks>
    /// Доступно кандидату или эксперту, участвующим в собеседовании.
    /// При отмене создаётся новая версия интервью с признаком отмены.
    /// Если одна из сторон уже отменила собеседование, другая сторона не может его отменить.
    /// </remarks>
    Task<CancelInterviewResponse> CancelInterviewAsync(CancelInterviewRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Подтвердить собеседование
    /// </summary>
    /// <remarks>
    /// Доступно кандидату или эксперту, участвующим в собеседовании.
    /// При подтверждении создаётся новая версия интервью с признаком подтверждения.
    /// Кандидат может подтвердить только если ещё не подтвердил.
    /// Эксперт может подтвердить только если ещё не подтвердил.
    /// </remarks>
    Task<ConfirmInterviewResponse> ConfirmInterviewAsync(ConfirmInterviewRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Изменить время собеседования
    /// </summary>
    /// <remarks>
    /// Доступно кандидату или эксперту, участвующим в собеседовании.
    /// Изменить время можно только если собеседование не отменено.
    /// При изменении времени кандидатом - подтверждение эксперта сбрасывается.
    /// При изменении времени экспертом - подтверждение кандидата сбрасывается.
    /// При изменении создаётся новая версия интервью.
    /// </remarks>
    Task<RescheduleInterviewResponse> RescheduleInterviewAsync(RescheduleInterviewRequest request, CancellationToken cancellationToken);
}
