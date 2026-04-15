using InterviewTraining.Api.Constants;
using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.GetAllExperts.V10;
using InterviewTraining.Application.ManageAvailableTime.V10;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Api.Controllers.Experts.V10;

[Route("api/v1/experts")]
[ApiController]
public class ExpertsController : BaseController<ExpertsController>
{
    private readonly ICustomMediator _mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="orderManager"></param>
    /// <param name="logger"></param>
    public ExpertsController(ICustomMediator mediator,
        ILogger<ExpertsController> logger
    )
        : base(logger)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить всех экспертов(может пользователь с ролью Кандидат или Эксперт) с фильтрацией
    /// </summary>
    [HttpPost]
    [Authorize(Roles = AuhConstants.RoleCandidateOrExpert)]
    public Task<GetAllExpertsResponse> GetByFilterAsync(GetAllExpertsRequest request, CancellationToken cancellationToken) =>
        _mediator.SendAsync(request, cancellationToken);

    /// <summary>
    /// Получить список доступного времени пользователя
    /// </summary>
    [HttpGet("{userId}/available-slots")]
    public async Task<GetAvailableTimeResponse> GetAvailableTime(string userId, CancellationToken cancellationToken)
    {
        var request = new GetAvailableTimeRequest
        {
            IdentityUserId = userId,
            CurrentIdentityUserId = CurrentUserId,
        };
        return await _mediator.SendAsync(request, cancellationToken);
    }
}
