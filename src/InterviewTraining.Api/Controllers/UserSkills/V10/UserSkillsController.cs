using InterviewTraining.Application.UpdateUserSkills.V10;
using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.GetSkillsTree.V10;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Api.Controllers.UserSkills.V10;

/// <summary>
/// Контроллер для управления навыками пользователей
/// </summary>
[Route("api/v1/user-skills")]
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
    /// Получить полное дерево навыков и их групп для пользователя
    /// </summary>
    [HttpGet("tree")]
    [Authorize]
    public async Task<GetSkillsTreeResponse> GetSkillsTree(CancellationToken cancellationToken)
    {
        var request = new GetSkillsTreeRequest();
        request.UserId = CurrentUserId;
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Получить полное дерево навыков и их групп для пользователя
    /// </summary>
    [HttpGet("tree/{userId}")]
    [Authorize]
    public async Task<GetSkillsTreeResponse> GetSkillsTree(string userId, CancellationToken cancellationToken)
    {
        var request = new GetSkillsTreeRequest();
        request.UserId = userId;
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Добавить навыки текущему пользователю
    /// </summary>
    ///<param name="skillIds">Массив идентификаторов навыков</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат операции</returns>
    [HttpPost]
    public async Task<ActionResult<UpdateUserSkillsResponse>> UpdateSkills(
        [FromBody] IEnumerable<Guid> skillIds,
        CancellationToken cancellationToken)
    {
        var request = new UpdateUserSkillsRequest
        {
            IdentityUserId = CurrentUserId,
            SkillIds = skillIds
        };

        var response = await _mediator.SendAsync(request, cancellationToken);
        return Ok(response);
    }
}
