using InterviewTraining.Application.CreateInterview.V10;
using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.GetMyInterviews.V10;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    [HttpGet("my")]
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
    ///<param name="request">Данные для создания собеседования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор созданного собеседования</returns>
    [HttpPost]
    [Authorize]
    public async Task<CreateInterviewResponse> CreateInterview([FromBody] CreateInterviewRequest request, CancellationToken cancellationToken)
    {
        request.CandidateId = CurrentUserId;
        return await _mediator.SendAsync(request, cancellationToken);
    }
}
