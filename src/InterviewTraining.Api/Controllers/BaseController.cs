using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InterviewTraining.Api.Controllers;

/// <summary>
/// BaseController
/// </summary>
public abstract class BaseController<TLogger> : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    protected readonly ILogger<TLogger> Logger;

    /// <summary>
    /// Constructor
    /// </summary>
    protected BaseController(ILogger<TLogger> logger)
    {
        Logger = logger;
    }

    /// <summary>
    /// Возвращает идентификатор текущего пользователя
    /// </summary>
    public string CurrentUserId
    {
        get
        {
            var firstClaim = User?.Claims?.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value)
                .FirstOrDefault();
            if (string.IsNullOrEmpty(firstClaim))
            {
                return User?.Claims?.Where(c => c.Type == "sub").Select(c => c.Value)
                    .FirstOrDefault();
            }

            return firstClaim;
        }
    }
}