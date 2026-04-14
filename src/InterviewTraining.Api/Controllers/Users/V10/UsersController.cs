using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.GetUserInfo.V10;
using InterviewTraining.Application.UpdateUserInfo.V10;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Api.Controllers.Users.V10;

[Route("api/v1/users")]
[ApiController]
public class UsersController : BaseController<UsersController>
{
    private readonly ICustomMediator _mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    ///<param name="mediator"></param>
    ///<param name="logger"></param>
    public UsersController(ICustomMediator mediator,
        ILogger<UsersController> logger
    )
        : base(logger)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить информацию о текущем пользователе
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<GetUserInfoResponse> GetUserInfo(CancellationToken cancellationToken)
    {
        var request = new GetUserInfoRequest();
        request.IdentityUserId = CurrentUserId;
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Обновить информацию о текущем пользователе
    /// </summary>
    [HttpPut]
    [Authorize]
    public async Task<UpdateUserInfoResponse> UpdateUserInfo([FromBody] UpdateUserInfoRequest request, CancellationToken cancellationToken)
    {
        request.IdentityUserId = CurrentUserId;
        return await _mediator.SendAsync(request, cancellationToken);
    }
}
