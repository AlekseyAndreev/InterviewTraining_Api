using InterviewTraining.Application.CancelInterview.V10;
using InterviewTraining.Application.ConfirmInterview.V10;
using InterviewTraining.Application.CreateChatMessage.V10;
using InterviewTraining.Application.CreateInterview.V10;
using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.GetInterviewInfo.V10;
using InterviewTraining.Application.GetMyInterviews.V10;
using InterviewTraining.Application.RescheduleInterview.V10;
using InterviewTraining.Application.UpdateChatMessage.V10;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Api.Controllers.Interviews.V10;

/// <summary>
/// Контроллер для работы с интервью
/// </summary>
[Route("api/v1/interviews")]
[ApiController]
public class InterviewsController : BaseController<InterviewsController>
{
    private readonly ICustomMediator _mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    ///<param name="mediator"></param>
    /// <param name="logger"></param>
    public InterviewsController(ICustomMediator mediator, ILogger<InterviewsController> logger)
        : base(logger)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить список моих интервью
    /// </summary>
    /// <remarks>
    /// Возвращает список всех интервью, где текущий пользователь является кандидатом или экспертом.
    /// Дата интервью возвращается в часовом поясе пользователя (из профиля) или UTC, если часовой пояс не задан.
    /// </remarks>
    ///<param name="cancellationToken">Токен отмены</param>
    /// <returns>Список интервью</returns>
    [HttpPost("my")]
    [Authorize]
    public async Task<GetMyInterviewsResponse> GetMyInterviews(CancellationToken cancellationToken)
    {
        var request = new GetMyInterviewsRequest
        {
            IdentityUserId = CurrentUserId
        };
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Создать новое собеседование
    /// </summary>
    /// <remarks>
    /// Создаёт собеседование между текущим пользователем (кандидатом) и указанным экспертом.
    /// Дата и время указываются в часовом поясе кандидата.
    /// После создания собеседование ожидает подтверждения от эксперта.
    /// </remarks>
    /// <param name="request">Данные для создания собеседования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор созданного собеседования</returns>
    [HttpPost]
    [Authorize]
    public async Task<CreateInterviewResponse> CreateInterview([FromBody] CreateInterviewRequest request, CancellationToken cancellationToken)
    {
        request.CandidateId = CurrentUserId;
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Получить информацию по собеседованию
    /// </summary>
    /// <remarks>
    /// Возвращает детальную информацию по собеседованию.
    /// Доступно только:
    /// - Кандидату (кто создал собеседование)
    /// - Эксперту (кто назначен экспертом в собеседовании)
    /// - Администратору
    /// </remarks>
    /// <param name="id">Идентификатор собеседования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Детальная информация по собеседованию</returns>
    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<GetInterviewInfoResponse> GetInterviewInfo(Guid id, CancellationToken cancellationToken)
    {
        var request = new GetInterviewInfoRequest
        {
            InterviewId = id,
            IdentityUserId = CurrentUserId,
            IsAdmin = IsAdmin
        };
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Отменить собеседование
    /// </summary>
    /// <remarks>
    /// Позволяет кандидату или эксперту отменить собеседование.
    /// При отмене создаётся новая версия интервью с признаком отмены.
    /// Если одна из сторон уже отменила собеседование, другая сторона не может его отменить.
    /// Причину отмены указывать необязательно.
    /// </remarks>
    /// <param name="id">Идентификатор собеседования</param>
    /// <param name="request">Данные для отмены собеседования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат отмены собеседования</returns>
    [HttpPost("{id:guid}/cancel")]
    [Authorize]
    public async Task<CancelInterviewResponse> CancelInterview(Guid id, [FromBody] CancelInterviewRequest request, CancellationToken cancellationToken)
    {
        request.IdentityUserId = CurrentUserId;
        request.InterviewId = id;
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Подтвердить собеседование
    /// </summary>
    /// <remarks>
    /// Позволяет кандидату или эксперту подтвердить собеседование.
    /// При подтверждении создаётся новая версия интервью с признаком подтверждения.
    /// Кандидат может подтвердить только если ещё не подтвердил.
    /// Эксперт может подтвердить только если ещё не подтвердил.
    /// Нельзя подтвердить отменённое собеседование.
    /// </remarks>
    /// <param name="id">Идентификатор собеседования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат подтверждения собеседования</returns>
    [HttpPost("{id:guid}/confirm")]
    [Authorize]
    public async Task<ConfirmInterviewResponse> ConfirmInterview(Guid id, CancellationToken cancellationToken)
    {
        var request = new ConfirmInterviewRequest
        {
            IdentityUserId = CurrentUserId,
            InterviewId = id
        };
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Изменить время собеседования
    /// </summary>
    /// <remarks>
    /// Позволяет кандидату или эксперту изменить время собеседования.
    /// Изменить время можно только если собеседование не отменено.
    /// При изменении времени кандидатом - подтверждение эксперта сбрасывается (требуется повторное подтверждение).
    /// При изменении времени экспертом - подтверждение кандидата сбрасывается (требуется повторное подтверждение).
    /// Новая дата и время указываются в часовом поясе пользователя.
    /// При изменении создаётся новая версия интервью.
    /// </remarks>
    /// <param name="id">Идентификатор собеседования</param>
    /// <param name="request">Данные для изменения времени</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат изменения времени собеседования</returns>
    [HttpPut("{id:guid}/reschedule")]
    [Authorize]
    public async Task<RescheduleInterviewResponse> RescheduleInterview(Guid id, [FromBody] RescheduleInterviewRequest request, CancellationToken cancellationToken)
    {
        request.IdentityUserId = CurrentUserId;
        request.InterviewId = id;
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Создать сообщение в чате интервью
    /// </summary>
    /// <remarks>
    /// Создаёт сообщение в чате собеседования.
    /// Доступно кандидату, эксперту (участвующим в собеседовании) или администратору.
    /// Тип отправителя определяется автоматически на основе роли пользователя в собеседовании.
    /// </remarks>
    /// <param name="id">Идентификатор собеседования</param>
    /// <param name="request">Данные сообщения</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор созданного сообщения</returns>
    [HttpPost("{id:guid}/chat/messages")]
    [Authorize]
    public async Task<CreateChatMessageResponse> CreateChatMessage(Guid id, [FromBody] CreateChatMessageRequest request, CancellationToken cancellationToken)
    {
        request.InterviewId = id;
        request.IdentityUserId = CurrentUserId;
        request.IsAdmin = IsAdmin;
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Редактировать сообщение в чате интервью
    /// </summary>
    /// <remarks>
    /// Редактирует сообщение в чате собеседования.
    /// Доступно только автору сообщения.
    /// При редактировании устанавливается признак IsEdited и дата модификации.
    /// </remarks>
    /// <param name="id">Идентификатор собеседования</param>
    /// <param name="messageId">Идентификатор сообщения</param>
    /// <param name="request">Новые данные сообщения</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат редактирования сообщения</returns>
    [HttpPut("{id:guid}/chat/messages/{messageId:guid}")]
    [Authorize]
    public async Task<UpdateChatMessageResponse> UpdateChatMessage(Guid id, Guid messageId, [FromBody] UpdateChatMessageRequest request, CancellationToken cancellationToken)
    {
        request.InterviewId = id;
        request.MessageId = messageId;
        request.IdentityUserId = CurrentUserId;
        request.IsAdmin = IsAdmin;
        return await _mediator.SendAsync(request, cancellationToken);
    }
}
