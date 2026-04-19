using InterviewTraining.Application.CancelInterview.V10;
using InterviewTraining.Application.ConfirmInterview.V10;
using InterviewTraining.Application.CreateChatMessage.V10;
using InterviewTraining.Application.CreateInterview.V10;
using InterviewTraining.Application.GetChatMessages.V10;
using InterviewTraining.Application.GetInterviewInfo.V10;
using InterviewTraining.Application.GetMyInterviews.V10;
using InterviewTraining.Application.RescheduleInterview.V10;
using InterviewTraining.Application.UpdateChatMessage.V10;
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

    /// <summary>
    /// Создать сообщение в чате интервью
    /// </summary>
    /// <remarks>
    /// Доступно кандидату, эксперту (участвующим в собеседовании) или администратору.
    /// Тип отправителя определяется автоматически на основе роли пользователя в собеседовании.
    /// </remarks>
    Task<CreateChatMessageResponse> CreateChatMessageAsync(CreateChatMessageRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Редактировать сообщение в чате интервью
    /// </summary>
    /// <remarks>
    /// Доступно только автору сообщения.
    /// При редактировании устанавливается признак IsEdited и дата модификации.
    /// </remarks>
    Task<UpdateChatMessageResponse> UpdateChatMessageAsync(UpdateChatMessageRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Получить сообщения чата интервью
    /// </summary>
    /// <remarks>
    /// Доступно кандидату, эксперту (участвующим в собеседовании) или администратору.
    /// </remarks>
    Task<GetChatMessagesResponse> GetChatMessagesAsync(GetChatMessagesRequest request, CancellationToken cancellationToken);
}
