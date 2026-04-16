using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.GetAllInterviewLanguages.V10;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Api.Controllers.InterviewLanguages.V10;

[Route("api/v1/interviews/languages")]
[ApiController]
public class InterviewLanguagesController : BaseController<InterviewLanguagesController>
{
    private readonly ICustomMediator _mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="orderManager"></param>
    /// <param name="logger"></param>
    public InterviewLanguagesController(ICustomMediator mediator,
        ILogger<InterviewLanguagesController> logger
    )
        : base(logger)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить все языки для интервью
    /// </summary>
    [HttpGet]
    [Authorize]
    public Task<InterviewLanguageResponse[]> GetAllAsync(GetAllInterviewLanguagesRequest request, CancellationToken cancellationToken) =>
        _mediator.SendAsync(new GetAllInterviewLanguagesRequest(), cancellationToken);
}
