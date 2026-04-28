using InterviewTraining.Api.Constants;
using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.UserChatMessage.V10.DeleteUserChatMessage;
using InterviewTraining.Application.UserChatMessage.V10.EditUserChatMessage;
using InterviewTraining.Application.UserChatMessage.V10.GetMessagesForExactUserToAdmin;
using InterviewTraining.Application.UserChatMessage.V10.GetUserChatMessagesWithAdmins;
using InterviewTraining.Application.UserChatMessage.V10.MarkUserChatMessageAsRead;
using InterviewTraining.Application.UserChatMessage.V10.SendUserChatMessage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Api.Controllers.UserChatMessages.V10;

///<summary>
/// Controller for user chat messages
///</summary>
[Route("api/v1/user-chat/messages")]
[ApiController]
public class UserChatMessagesController : BaseController<UserChatMessagesController>
{
    private readonly ICustomMediator _mediator;

    ///<summary>
    /// Constructor
    ///</summary>
    public UserChatMessagesController(ICustomMediator mediator, ILogger<UserChatMessagesController> logger)
        : base(logger)
    {
        _mediator = mediator;
    }

    ///<summary>
    /// Send a message to another user
    ///</summary>
    [HttpPost]
    [Authorize]
    public async Task<SendUserChatMessageResponse> SendMessage(
        [FromBody] SendUserChatMessageRequest request,
        CancellationToken cancellationToken)
    {
        request.CurrentIdentityUserId = CurrentUserId;
        request.IsAdmin = IsAdmin;
        return await _mediator.SendAsync(request, cancellationToken);
    }

    ///<summary>
    /// Edit a message
    ///</summary>
    [HttpPut("{messageId:guid}")]
    [Authorize]
    public async Task<EditUserChatMessageResponse> EditMessage(
        Guid messageId,
        [FromBody] EditUserChatMessageRequest request,
        CancellationToken cancellationToken)
    {
        request.MessageId = messageId;
        request.IdentityUserId = CurrentUserId;
        return await _mediator.SendAsync(request, cancellationToken);
    }

    ///<summary>
    /// Delete a message
    ///</summary>
    [HttpDelete("{messageId:guid}")]
    [Authorize]
    public async Task<DeleteUserChatMessageResponse> DeleteMessage(
        Guid messageId,
        CancellationToken cancellationToken)
    {
        var request = new DeleteUserChatMessageRequest
        {
            MessageId = messageId,
            IdentityUserId = CurrentUserId
        };
        return await _mediator.SendAsync(request, cancellationToken);
    }

    ///<summary>
    /// Mark messages as read
    ///</summary>
    [HttpPost("{messageId:guid}/read")]
    [Authorize]
    public async Task<MarkUserChatMessageAsReadResponse> MarkAsRead(
        Guid messageId,
        CancellationToken cancellationToken)
    {
        var request = new MarkUserChatMessageAsReadRequest
        {
            IdentityUserId = CurrentUserId,
            MessageId = messageId
        };
        return await _mediator.SendAsync(request, cancellationToken);
    }

    ///<summary>
    /// Get messages for exact user to admin
    ///</summary>
    [HttpGet("for-admin")]
    [Authorize(Roles = AuhConstants.RoleAdmin)]
    public async Task<GetMessagesForExactUserToAdminResponse> GetMessagesForExactUserToAdmin(
        [FromQuery]string userId,
        CancellationToken cancellationToken = default)
    {
        var request = new GetMessagesForExactUserToAdminRequest
        {
            CurrentIdentityUserId = CurrentUserId,
            ChatWithIdentityUserId = userId,
            IsAdmin = IsAdmin,
        };
        return await _mediator.SendAsync(request, cancellationToken);
    }

    ///<summary>
    /// Get messages with administrators
    ///</summary>
    [HttpGet("with-admins")]
    [Authorize]
    public async Task<GetUserChatMessagesWithAdminsResponse> GetMessagesWithAdmins(
        CancellationToken cancellationToken = default)
    {
        var request = new GetUserChatMessagesWithAdminsRequest
        {
            IdentityUserId = CurrentUserId,
        };
        return await _mediator.SendAsync(request, cancellationToken);
    }
}
