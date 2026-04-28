using InterviewTraining.Application.Exceptions;
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

        var result = await _unitOfWork.UserChatMessages.SoftDeleteAsync(request.MessageId, user.Id, cancellationToken);
        if (!result)
        {
            _logger.LogWarning("Message {MessageId} not found or not owned by user {UserId}",
                request.MessageId, user.Id);
            throw new EntityNotFoundException("Message not found or you don't have permission to delete it");
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Message {MessageId} deleted by user {UserId}", request.MessageId, user.Id);

        return new DeleteUserChatMessageResponse
        {
            Success = true,
            MessageId = request.MessageId
        };
    }
}
