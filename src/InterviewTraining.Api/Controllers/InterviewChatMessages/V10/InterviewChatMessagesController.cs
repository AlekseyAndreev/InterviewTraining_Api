using InterviewTraining.Application.CreateInterviewChatMessage.V10;
using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.GetInterviewChatMessages.V10;
using InterviewTraining.Application.UpdateInterviewChatMessage.V10;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Api.Controllers.InterviewChatMessages.V10;

/// <summary>
/// Контроллер для работы с интервью
/// </summary>
[Route("api/v1/interviews")]
[ApiController]
public class InterviewChatMessagesController : BaseController<InterviewChatMessagesController>
{
    private readonly ICustomMediator _mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    ///<param name="mediator"></param>
    /// <param name="logger"></param>
    public InterviewChatMessagesController(ICustomMediator mediator, ILogger<InterviewChatMessagesController> logger)
        : base(logger)
    {
        _mediator = mediator;
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
    public async Task<CreateInterviewChatMessageResponse> CreateInterviewChatMessage(Guid id, [FromBody] CreateInterviewChatMessageRequest request, CancellationToken cancellationToken)
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
    public async Task<UpdateInterviewChatMessageResponse> UpdateInterviewChatMessage(Guid id, Guid messageId, [FromBody] UpdateInterviewChatMessageRequest request, CancellationToken cancellationToken)
    {
        request.InterviewId = id;
        request.MessageId = messageId;
        request.IdentityUserId = CurrentUserId;
        request.IsAdmin = IsAdmin;
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Получить сообщения чата интервью
    /// </summary>
    /// <remarks>
    /// Возвращает все сообщения чата собеседования.
    /// Доступно кандидату, эксперту (участвующим в собеседовании) или администратору.
    /// </remarks>
    /// <param name="id">Идентификатор собеседования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список сообщений чата</returns>
    [HttpGet("{id:guid}/chat/messages")]
    [Authorize]
    public async Task<GetInterviewChatMessagesResponse> GetInterviewChatMessages(Guid id, CancellationToken cancellationToken)
    {
        var request = new GetInterviewChatMessagesRequest
        {
            InterviewId = id,
            IdentityUserId = CurrentUserId,
            IsAdmin = IsAdmin
        };
        return await _mediator.SendAsync(request, cancellationToken);
    }
}