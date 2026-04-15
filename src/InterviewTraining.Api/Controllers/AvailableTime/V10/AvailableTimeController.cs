using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.ManageAvailableTime.V10;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Api.Controllers.AvailableTime.V10;

[Route("api/v1/users/me/available-time")]
[ApiController]
[Authorize]
public class AvailableTimeController : BaseController<AvailableTimeController>
{
    private readonly ICustomMediator _mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    ///<param name="mediator"></param>
    ///<param name="logger"></param>
    public AvailableTimeController(ICustomMediator mediator, ILogger<AvailableTimeController> logger)
        : base(logger)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создать запись доступного времени
    /// </summary>
    [HttpPost]
    public async Task<CreateAvailableTimeResponse> CreateAvailableTime(
        [FromBody] CreateAvailableTimeRequest request,
        CancellationToken cancellationToken)
    {
        request.IdentityUserId = CurrentUserId;
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Получить список доступного времени текущего пользователя
    /// </summary>
    [HttpGet]
    public async Task<GetAvailableTimeResponse> GetAvailableTime(CancellationToken cancellationToken)
    {
        var request = new GetAvailableTimeRequest
        {
            IdentityUserId = CurrentUserId
        };
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Удалить запись доступного времени
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<DeleteAvailableTimeResponse> DeleteAvailableTime(
        Guid id,
        CancellationToken cancellationToken)
    {
        var request = new DeleteAvailableTimeRequest
        {
            IdentityUserId = CurrentUserId,
            Id = id
        };
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Обновить запись доступного времени
    /// </summary>
    [HttpPut("{availableTimeId}")]
    public async Task<UpdateAvailableTimeResponse> UpdateAvailableTime(
        Guid availableTimeId,
        [FromBody] UpdateAvailableTimeRequest request,
        CancellationToken cancellationToken)
    {
        request.IdentityUserId = CurrentUserId;
        request.AvailableTimeId = availableTimeId;
        return await _mediator.SendAsync(request, cancellationToken);
    }
}
