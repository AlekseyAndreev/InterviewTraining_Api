using System;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.SignalR;
using InterviewTraining.Application.UpdateChatMessage.V10;
using Microsoft.Extensions.Logging;

namespace InterviewTraining.Infrastructure.Services;

/// <summary>
/// Сервис для работы с интервью
/// </summary>
public partial class InterviewService
{
    /// <summary>
    /// Редактировать сообщение в чате интервью
    /// </summary>
    public async Task<UpdateChatMessageResponse> UpdateChatMessageAsync(UpdateChatMessageRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.IdentityUserId, cancellationToken);
        if (currentUser == null)
        {
            _logger.LogWarning("Не найдена информация по пользователю {UserId}", request.IdentityUserId);
            throw new BusinessLogicException("Не найдена информация по пользователю");
        }

        var chatMessage = await _unitOfWork.ChatMessages.GetByIdAndSenderAsync(request.MessageId, currentUser.Id);
        if (chatMessage == null)
        {
            _logger.LogWarning("Сообщение {MessageId} не найдено или пользователь {UserId} не является его автором",
                request.MessageId, currentUser.Id);
            throw new EntityNotFoundException("Сообщение не найдено или вы не являетесь его автором");
        }

        if (chatMessage.InterviewId != request.InterviewId)
        {
            _logger.LogWarning("Сообщение {MessageId} не принадлежит интервью {InterviewId}",
                request.MessageId, request.InterviewId);
            throw new BusinessLogicException("Сообщение не принадлежит указанному собеседованию");
        }

        chatMessage.MessageText = request.MessageText;
        chatMessage.IsEdited = true;
        chatMessage.ModifiedUtc = DateTime.UtcNow;

        _unitOfWork.ChatMessages.Update(chatMessage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.NotifyChatMessageUpdatedAsync(new InterviewChatMessageNotificationDto
        {
            Id = chatMessage.Id,
            InterviewId = chatMessage.InterviewId,
            From = (int)chatMessage.SenderType,
            Text = chatMessage.MessageText,
            CreatedUtc = chatMessage.CreatedUtc,
            IsEdited = chatMessage.IsEdited,
            ModifiedUtc = chatMessage.ModifiedUtc
        });

        _logger.LogInformation("Отредактировано сообщение {MessageId} в чате интервью {InterviewId}",
            chatMessage.Id, chatMessage.InterviewId);

        return new UpdateChatMessageResponse
        {
            MessageId = chatMessage.Id,
            ModifiedUtc = chatMessage.ModifiedUtc.Value,
            IsEdited = chatMessage.IsEdited
        };
    }
}
