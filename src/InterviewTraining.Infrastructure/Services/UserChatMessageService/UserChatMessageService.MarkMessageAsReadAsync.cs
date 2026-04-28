using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.UserChatMessage.V10.MarkUserChatMessageAsRead;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Services;

///<summary>
/// Service for user chat messages
///</summary>
public partial class UserChatMessageService
{
    ///<summary>
    /// Mark messages as read
    ///</summary>
    public async Task<MarkUserChatMessageAsReadResponse> MarkMessageAsReadAsync(
        MarkUserChatMessageAsReadRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.IdentityUserId, cancellationToken);
        if (user == null)
        {
            _logger.LogWarning("User not found: {UserId}", request.IdentityUserId);
            throw new BusinessLogicException("User not found");
        }

        var message = await _unitOfWork.UserChatMessages.GetByIdWithUsersAsync(request.MessageId, cancellationToken);
        if (message == null)
        {
            _logger.LogWarning("Message not found: {MessageId}", request.MessageId);
            throw new EntityNotFoundException("Message not found");
        }

        if (message.SenderUserId != user.Id)
        {
            _logger.LogWarning("User {UserId} is not authorized to edit message {MessageId}", user.Id, request.MessageId);
            throw new BusinessLogicException("You can only edit your own messages");
        }

        if (message.IsDeleted)
        {
            _logger.LogWarning("Cannot mark as read deleted message: {MessageId}", request.MessageId);
            throw new BusinessLogicException("Cannot mark as read deleted message");
        }

        message.IsRead = true;
        message.ModifiedUtc = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Message from marked as read by {UserId}", user.Id);

        return new MarkUserChatMessageAsReadResponse
        {
            Success = true,
        };
    }
}
