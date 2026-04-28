using System;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.SignalR;
using InterviewTraining.Application.UpdateInterviewChatMessage.V10;
using Microsoft.Extensions.Logging;

namespace InterviewTraining.Infrastructure.Services;

/// <summary>
/// Сервис для работы с интервью
/// </summary>
public partial class InterviewChatMessageService
{
    /// <summary>
    /// Редактировать сообщение в чате интервью
    /// </summary>
    public async Task<UpdateInterviewChatMessageResponse> UpdateInterviewChatMessageAsync(UpdateInterviewChatMessageRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.IdentityUserId, cancellationToken);
        if (currentUser == null)
        {
            _logger.LogWarning("Не найдена информация по пользователю {UserId}", request.IdentityUserId);
            throw new BusinessLogicException("Не найдена информация по пользователю");
        }

        var interviewChatMessage = await _unitOfWork.InterviewChatMessages.GetByIdAndSenderAsync(request.MessageId, currentUser.Id);
        if (interviewChatMessage == null)
        {
            _logger.LogWarning("Сообщение {MessageId} не найдено или пользователь {UserId} не является его автором",
                request.MessageId, currentUser.Id);
            throw new EntityNotFoundException("Сообщение не найдено или вы не являетесь его автором");
        }

        if (interviewChatMessage.InterviewId != request.InterviewId)
        {
            _logger.LogWarning("Сообщение {MessageId} не принадлежит интервью {InterviewId}",
                request.MessageId, request.InterviewId);
            throw new BusinessLogicException("Сообщение не принадлежит указанному собеседованию");
        }

        interviewChatMessage.MessageText = request.MessageText;
        interviewChatMessage.IsEdited = true;
        interviewChatMessage.ModifiedUtc = DateTime.UtcNow;

        _unitOfWork.InterviewChatMessages.Update(interviewChatMessage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationProvider.NotifyChatMessageUpdatedAsync(new InterviewChatMessageNotificationDto
        {
            Id = interviewChatMessage.Id,
            InterviewId = interviewChatMessage.InterviewId,
            From = (int)interviewChatMessage.SenderType,
            Text = interviewChatMessage.MessageText,
            CreatedUtc = interviewChatMessage.CreatedUtc,
            IsEdited = interviewChatMessage.IsEdited,
            ModifiedUtc = interviewChatMessage.ModifiedUtc
        });

        _logger.LogInformation("Отредактировано сообщение {MessageId} в чате интервью {InterviewId}",
            interviewChatMessage.Id, interviewChatMessage.InterviewId);

        return new UpdateInterviewChatMessageResponse
        {
            MessageId = interviewChatMessage.Id,
            ModifiedUtc = interviewChatMessage.ModifiedUtc.Value,
            IsEdited = interviewChatMessage.IsEdited
        };
    }
}
