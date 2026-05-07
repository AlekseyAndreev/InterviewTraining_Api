using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.SignalR;
using InterviewTraining.Application.UserChatMessage.V10.DeleteUserChatMessage;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Services;

///<summary>
/// Service for user chat messages
///</summary>
public partial class UserChatMessageService
{
    ///<summary>
    /// Delete message (soft delete, only own messages)
    ///</summary>
    public async Task<DeleteUserChatMessageResponse> DeleteMessageAsync(
        DeleteUserChatMessageRequest request,
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
            throw new EntityNotFoundException("UserChatMessage");
        }

        if (message.SenderUserId != user.Id && message.SenderUser?.IsAdmin != true && !user.IsAdmin)
        {
            _logger.LogWarning("User {UserId} is not authorized to edit message {MessageId}", user.Id, request.MessageId);
            throw new BusinessLogicException("You can only edit your own messages");
        }

        var result = await _unitOfWork.UserChatMessages.SoftDeleteAsync(request.MessageId, user.Id, cancellationToken);
        if (!result)
        {
            _logger.LogWarning("Message {MessageId} not found or not owned by user {UserId}",
                request.MessageId, user.Id);
            throw new EntityNotFoundException("UserChatMessage");
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Message {MessageId} deleted by user {UserId}", request.MessageId, user.Id);

        await _userWithAdminChatNotificationProvider.NotifyChatMessageDeletedAsync(new UserWithAdminChatMessageNotificationDto
        {
            Id = message.Id,
            Text = message.MessageText,
            CreatedUtc = message.CreatedUtc,
            IsEdited = message.IsEdited,
            ModifiedUtc = message.ModifiedUtc,
            UserId = user.IsAdmin ? message.ReceiverUser.IdentityUserId : message.SenderUser.IdentityUserId
        });


        return new DeleteUserChatMessageResponse
        {
            Success = true,
            MessageId = request.MessageId
        };
    }
}
