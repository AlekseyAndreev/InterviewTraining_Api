using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.GetSkillsTree.V10;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InterviewTraining.Api.Controllers.Skills.V10;

[Route("api/v1/skills")]
[ApiController]
public class SkillsController : BaseController<SkillsController>
{
    private readonly ICustomMediator _mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    ///<param name="mediator"></param>
    ///<param name="logger"></param>
    public SkillsController(ICustomMediator mediator, ILogger<SkillsController> logger)
        : base(logger)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить полное дерево навыков и их групп
    /// </summary>
    [HttpGet("tree")]
    [Authorize]
    public async Task<GetSkillsTreeResponse> GetSkillsTree(CancellationToken cancellationToken)
    {
        return await _mediator.SendAsync(new GetSkillsTreeRequest(), cancellationToken);
    }
}
