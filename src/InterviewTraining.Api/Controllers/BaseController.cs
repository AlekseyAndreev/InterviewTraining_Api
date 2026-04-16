using InterviewTraining.Api.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;

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

    /// <summary>
    /// Возвращает является ли текущий пользователь кандидатом(у него есть роль кандидат в токене)
    /// </summary>
    public bool IsCandidate
    {
        get
        {
            var roles = User?.Claims?.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value)
                .ToArray();
            if (roles == null || !roles.Any())
            {
                return false;
            }

            return roles.Contains(AuhConstants.RoleCandidate);
        }
    }

    /// <summary>
    /// Возвращает является ли текущий пользователь экспертом(у него есть роль эксперт в токене)
    /// </summary>
    public bool IsExpert
    {
        get
        {
            var roles = User?.Claims?.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value)
                .ToArray();
            if (roles == null || !roles.Any())
            {
                return false;
            }

            return roles.Contains(AuhConstants.RoleExpert);
        }
    }

    /// <summary>
    /// Возвращает является ли текущий пользователь администратором(у него есть роль админ в токене)
    /// </summary>
    public bool IsAdmin
    {
        get
        {
            var roles = User?.Claims?.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value)
                .ToArray();
            if (roles == null || !roles.Any())
            {
                return false;
            }

            return roles.Contains(AuhConstants.RoleAdmin);
        }
    }
}
