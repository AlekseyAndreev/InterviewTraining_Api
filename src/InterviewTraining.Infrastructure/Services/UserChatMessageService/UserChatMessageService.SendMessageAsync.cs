using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.UserChatMessage.V10.SendUserChatMessage;
using InterviewTraining.Domain;
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
    private readonly string DefaultAdminUserId = Environment.GetEnvironmentVariable("DEFAULT_ADMIN_IDENTITY_USER_ID");

    ///<summary>
    /// Send message from one user to another
    ///</summary>
    public async Task<SendUserChatMessageResponse> SendMessageAsync(
        SendUserChatMessageRequest request,
        CancellationToken cancellationToken)
    {
        var sender = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.CurrentIdentityUserId, cancellationToken);
        if (sender == null)
        {
            _logger.LogWarning("User not found: {UserId}", request.CurrentIdentityUserId);
            throw new BusinessLogicException("User not found");
        }

        AdditionalUserInfo receiver;
        if (request.IsAdmin)
        {
            receiver = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.ReceiverIdentityUserId, cancellationToken);
            if (receiver == null)
            {
                _logger.LogWarning("Receiver not found: {ReceiverId}", request.ReceiverIdentityUserId);
                throw new EntityNotFoundException("Receiver not found");
            }
        }
        else 
        {
            receiver = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(DefaultAdminUserId, cancellationToken);
            if (receiver == null)
            {
                _logger.LogWarning("DefaultAdminUserId receiver not found: {ReceiverId}", DefaultAdminUserId);
                throw new EntityNotFoundException("Receiver not found");
            }
        }

        var message = new UserChatMessage
        {
            Id = Guid.NewGuid(),
            SenderUserId = sender.Id,
            ReceiverUserId = receiver.Id,
            MessageText = request.MessageText,
            IsEdited = false,
            IsRead = false,
            IsDeleted = false,
            CreatedUtc = DateTime.UtcNow
        };

        await _unitOfWork.UserChatMessages.AddAsync(message);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Message {MessageId} sent from {SenderId} to {ReceiverId}",
            message.Id, sender.Id, receiver.Id);

        return new SendUserChatMessageResponse
        {
            MessageId = message.Id,
            CreatedUtc = message.CreatedUtc
        };
    }
}
