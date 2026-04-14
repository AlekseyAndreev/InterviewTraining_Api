using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.AddUserSkills.V10;
using InterviewTraining.Application.CustomMediatorLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InterviewTraining.Api.Controllers.UserSkills.V10;

/// <summary>
/// Контроллер для управления навыками пользователей
/// </summary>
[Route("api/v1/userskills")]
[ApiController]
[Authorize]
public class UserSkillsController : BaseController<UserSkillsController>
{
    private readonly ICustomMediator _mediator;

    /// <summary>
    /// Конструктор
    /// </summary>
    public UserSkillsController(
        ILogger<UserSkillsController> logger,
        ICustomMediator mediator) : base(logger)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Добавить навыки текущему пользователю
    /// </summary>
    ///<param name="skillIds">Массив идентификаторов навыков</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат операции</returns>
    [HttpPost]
    public async Task<ActionResult<AddUserSkillsResponse>> AddSkills(
        [FromBody] IEnumerable<Guid> skillIds,
        CancellationToken cancellationToken)
    {
        var request = new AddUserSkillsRequest
        {
            IdentityUserId = CurrentUserId,
            SkillIds = skillIds
        };

        var response = await _mediator.SendAsync(request, cancellationToken);
        return Ok(response);
    }
}
