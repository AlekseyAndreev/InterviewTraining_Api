using InterviewTraining.Api.Constants;
using InterviewTraining.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Api.Controllers.UserSync.V10;

/// <summary>
/// Контроллер для синхронизации пользователей (внутренний API)
/// </summary>
[ApiController]
[Route("api/internal/users")]
public class UserSyncController : ControllerBase
{
    private readonly IUserSyncService _userSyncService;
    private readonly ILogger<UserSyncController> _logger;
    private readonly IConfiguration _configuration;

    public UserSyncController(
        IUserSyncService userSyncService,
        ILogger<UserSyncController> logger,
        IConfiguration configuration)
    {
        _userSyncService = userSyncService;
        _logger = logger;
        _configuration = configuration;
    }

    /// <summary>
    /// Синхронизировать пользователя из IdentityServer
    /// </summary>
    [HttpPost("sync")]
    [AllowAnonymous]
    public async Task<IActionResult> SyncUser([FromBody] UserSyncRequest request, CancellationToken cancellationToken)
    {
        if (!ValidateApiKey())
        {
            _logger.LogWarning("Попытка доступа к UserSync без авторизации");
            return Unauthorized();
        }

        if (string.IsNullOrEmpty(request.IdentityUserId))
        {
            return BadRequest(new { error = "IdentityUserId is required" });
        }

        try
        {
            await _userSyncService.SyncUserAsync(request.IdentityUserId, request.IsCandidate, request.IsExpert, cancellationToken);

            _logger.LogInformation("Пользователь {UserId} успешно синхронизирован", request.IdentityUserId);
            return Ok(new { success = true, identityUserId = request.IdentityUserId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при синхронизации пользователя {UserId}", request.IdentityUserId);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    private bool ValidateApiKey()
    {
        var expectedApiKey = _configuration["UserSync:ApiKey"];
        
        if (string.IsNullOrEmpty(expectedApiKey))
        {
            _logger.LogError("UserSync:ApiKey не настроен - доступ разрешен без проверки");
            return false;
        }

        return Request.Headers.TryGetValue(ApiKeyConstants.ApiKeyHeaderName, out var providedApiKey) 
               && providedApiKey == expectedApiKey;
    }
}

/// <summary>
/// Модель запроса синхронизации пользователя
/// </summary>
public class UserSyncRequest
{
    /// <summary>
    /// Идентификатор пользователя в IdentityServer
    /// </summary>
    [JsonPropertyName("identityUserId")]
    public string IdentityUserId { get; set; } = string.Empty;
    
    /// <summary>
    /// Является ли кандидатом
    /// </summary>
    [JsonPropertyName("isCandidate")]
    public bool IsCandidate { get; set; }
    
    /// <summary>
    /// Является ли экспертом
    /// </summary>
    [JsonPropertyName("isExpert")]
    public bool IsExpert { get; set; }
}
