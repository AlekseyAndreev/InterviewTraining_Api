using InterviewTraining.Application.AllLlmInterviews.V10;
using InterviewTraining.Application.AskLlm.V10;
using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.StartLlm.V10;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Api.Controllers.Llm.V10;

/// <summary>
/// Контроллер для работы с LLM
/// </summary>
[Route("api/v1/llm")]
[ApiController]
public class LlmController : BaseController<LlmController>
{
    private readonly ICustomMediator _mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="logger"></param>
    public LlmController(ICustomMediator mediator, ILogger<LlmController> logger)
        : base(logger)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить список всех собеседований с ИИ
    /// </summary>
    /// <param name="request">Данные запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Ответ</returns>
    [HttpGet("all-interviews")]
    [Authorize]
    public async Task<AllLlmInterviewsResponse[]> AllInterviewsAsync(CancellationToken cancellationToken)
    {
        return await _mediator.SendAsync(new AllLlmInterviewsRequest(), cancellationToken);
    }

    /// <summary>
    /// Отправить запрос к языковой модели
    /// </summary>
    /// <remarks>
    /// Отправляет текстовый запрос к LLM и возвращает ответ модели.
    /// Можно указать системный промпт для контекстуализации ответа.
    /// </remarks>
    /// <param name="request">Данные запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Ответ языковой модели</returns>
    [HttpPost("start")]
    [Authorize]
    public async Task<StartLlmResponse> StartAsync([FromBody] StartLlmRequest request, CancellationToken cancellationToken)
    {
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Отправить запрос к языковой модели
    /// </summary>
    /// <remarks>
    /// Отправляет текстовый запрос к LLM и возвращает ответ модели.
    /// Можно указать системный промпт для контекстуализации ответа.
    /// </remarks>
    /// <param name="request">Данные запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Ответ языковой модели</returns>
    [HttpPost("ask")]
    [Authorize]
    public async Task<AskLlmResponse> AskAsync([FromBody] AskLlmRequest request, CancellationToken cancellationToken)
    {
        return await _mediator.SendAsync(request, cancellationToken);
    }
}