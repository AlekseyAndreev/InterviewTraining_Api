using InterviewTraining.Api.Constants;
using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.DeleteUserNotification.V10;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.GetAllUsersForAdmin.V10;
using InterviewTraining.Application.GetUserInfo.V10;
using InterviewTraining.Application.GetUserNotifications.V10;
using InterviewTraining.Application.ManageAvailableTime.V10;
using InterviewTraining.Application.UpdateUserInfo.V10;
using InterviewTraining.Application.UpdateUserNotification.V10;
using InterviewTraining.Application.UpdateUserTimeZone.V10;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Api.Controllers.Users.V10;

[Route("api/v1/users")]
[ApiController]
public class UsersController : BaseController<UsersController>
{
    private readonly ICustomMediator _mediator;

    private static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
    private const int MaxPhotoSizeBytes = 10 * 1024 * 1024; // 10 MB

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
    /// Получить информацию о пользователе
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<GetUserInfoResponse> GetUserInfo(CancellationToken cancellationToken)
    {
        var request = new GetUserInfoRequest();
        request.IdentityUserId = CurrentUserId;
        return await _mediator.SendAsync(request, cancellationToken);
    }

    ///<summary>
    /// Получить информацию о пользователе
    /// </summary>
    [HttpGet("{userId}")]
    [Authorize]
    public async Task<GetUserInfoResponse> GetUserInfo(string userId, CancellationToken cancellationToken)
    {
        var request = new GetUserInfoRequest();
        if (string.IsNullOrEmpty(userId))
        {
            request.IdentityUserId = CurrentUserId;
        }
        else
        {
            request.IdentityUserId = userId;
        }
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Получить список доступного времени пользователя
    /// </summary>
    [HttpGet("{userId}/available-times")]
    public async Task<GetAvailableTimeResponse> GetAvailableTime(string userId, CancellationToken cancellationToken)
    {
        var request = new GetAvailableTimeRequest
        {
            IdentityUserId = userId,
            CurrentIdentityUserId = CurrentUserId,
        };
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Обновить информацию о текущем пользователе
    /// </summary>
    [HttpPut]
    [Authorize]
    [RequestSizeLimit(MaxPhotoSizeBytes)]
    public async Task<UpdateUserInfoResponse> UpdateUserInfo(
        [FromForm] string fullName,
        [FromForm] string shortDescription,
        [FromForm] string description,
        [FromForm] decimal? interviewPrice,
        [FromForm] Guid? currencyId,
        IFormFile photo,
        CancellationToken cancellationToken)
    {
        byte[] photoBytes = null;

        if (photo != null)
        {
            ValidatePhotoFile(photo);
            photoBytes = await ReadFileBytesAsync(photo);
        }

        var request = new UpdateUserInfoRequest
        {
            IdentityUserId = CurrentUserId,
            FullName = fullName,
            ShortDescription = shortDescription,
            Description = description,
            Photo = photoBytes,
            InterviewPrice = interviewPrice,
            CurrencyId = currencyId,
            IsExpert = IsExpert,
        };

        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Обновить временную зону у текущего пользователе
    /// </summary>
    [HttpPut("time-zone")]
    [Authorize]
    public async Task<UpdateUserTimeZoneResponse> UpdateUserTimeZoneInfo(
        [FromBody] UpdateUserTimeZoneRequest request,
        CancellationToken cancellationToken)
    {
        request.IdentityUserId = CurrentUserId;
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Получить уведомления текущего пользователя
    /// </summary>
    [HttpGet("me/notifications")]
    [Authorize]
    public async Task<GetUserNotificationsResponse> GetUserNotifications(CancellationToken cancellationToken)
    {
        var request = new GetUserNotificationsRequest();
        request.IdenitityUserId = CurrentUserId;
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Удалить уведомление
    /// </summary>
    [HttpDelete("me/notifications/{notificationId}")]
    [Authorize]
    public async Task<DeleteUserNotificationResponse> DeleteNotification(Guid notificationId, CancellationToken cancellationToken)
    {
        var request = new DeleteUserNotificationRequest
        {
            NotificationId = notificationId,
            IdentityUserId = CurrentUserId,
        };
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Пометить уведомление как прочитанное
    /// </summary>
    [HttpPut("me/notifications/{notificationId}/read")]
    [Authorize]
    public async Task<UpdateUserNotificationResponse> MarkNotificationAsRead(Guid notificationId, CancellationToken cancellationToken)
    {
        var request = new UpdateUserNotificationRequest
        {
            NotificationId = notificationId,
            IsRead = true,
            IdentityUserId = CurrentUserId,
        };
        return await _mediator.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Пометить уведомление как непрочитанное
    /// </summary>
    [HttpPut("me/notifications/{notificationId}/unread")]
    [Authorize]
    public async Task<UpdateUserNotificationResponse> MarkNotificationAsUnread(Guid notificationId, CancellationToken cancellationToken)
    {
        var request = new UpdateUserNotificationRequest
        {
            NotificationId = notificationId,
            IsRead = false,
            IdentityUserId = CurrentUserId,
        };
        return await _mediator.SendAsync(request, cancellationToken);
    }

    ///<summary>
    /// Get all users (admin only)
    ///</summary>
    [HttpGet("all")]
    [Authorize(Roles = AuhConstants.RoleAdmin)]
    public async Task<GetAllUsersForAdminResponse> GetAllUsers(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string searchFilter = null,
        CancellationToken cancellationToken = default)
    {
        var request = new GetAllUsersForAdminRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            SearchFilter = searchFilter,
            IsAdmin = IsAdmin,
        };
        return await _mediator.SendAsync(request, cancellationToken);
    }

    private void ValidatePhotoFile(IFormFile photo)
    {
        var extension = Path.GetExtension(photo.FileName).ToLowerInvariant();

        if (string.IsNullOrEmpty(extension) || !AllowedImageExtensions.Contains(extension))
        {
            throw new BusinessLogicException($"Недопустимый формат файла. Разрешённые форматы: {string.Join(", ", AllowedImageExtensions)}");
        }

        if (photo.Length > MaxPhotoSizeBytes)
        {
            throw new BusinessLogicException($"Размер файла превышает максимально допустимый (5 МБ)");
        }
    }

    private static async Task<byte[]> ReadFileBytesAsync(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
}
