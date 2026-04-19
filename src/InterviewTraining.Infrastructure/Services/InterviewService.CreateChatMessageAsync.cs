using System;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.CreateChatMessage.V10;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.SignalR;
using InterviewTraining.Domain;
using Microsoft.Extensions.Logging;

namespace InterviewTraining.Infrastructure.Services;

/// <summary>
/// Сервис для работы с интервью
/// </summary>
public partial class InterviewService
{
    /// <summary>
    /// Создать сообщение в чате интервью
    /// </summary>
    public async Task<CreateChatMessageResponse> CreateChatMessageAsync(CreateChatMessageRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.IdentityUserId, cancellationToken);
        if (currentUser == null)
        {
            _logger.LogWarning("Не найдена информация по пользователю {UserId}", request.IdentityUserId);
            throw new BusinessLogicException("Не найдена информация по пользователю");
        }

        var interview = await _unitOfWork.Interviews.GetByIdAsync(request.InterviewId);
        if (interview == null)
        {
            _logger.LogWarning("Интервью с идентификатором {InterviewId} не найдено", request.InterviewId);
            throw new EntityNotFoundException("Собеседование не найдено");
        }

        var senderType = DetermineSenderType(interview, currentUser, request.IsAdmin);
        if (senderType == MessageSenderType.Unknown)
        {
            _logger.LogWarning("Пользователь {UserId} не имеет доступа к интервью {InterviewId}",
                currentUser.Id, interview.Id);
            throw new BusinessLogicException("У вас нет доступа к этому собеседованию");
        }

        var (chatMessageId, chatCreatedAtUtc) = await CreateChatMessageInternal(interview.Id, senderType, currentUser.Id, request.MessageText, cancellationToken);

        _logger.LogInformation("Создано сообщение {MessageId} в чате интервью {InterviewId} от пользователя {UserId}",
            chatMessageId, interview.Id, currentUser.Id);

        return new CreateChatMessageResponse
        {
            MessageId = chatMessageId,
            CreatedUtc = chatCreatedAtUtc
        };
    }

    private async Task<(Guid chatMessageId, DateTime chatCreatedAtUtc)> CreateChatMessageInternal(Guid interviewId, MessageSenderType senderType, Guid? senderUserId, string text, CancellationToken cancellationToken)
    {
        var chatMessage = new ChatMessage
        {
            InterviewId = interviewId,
            SenderType = senderType,
            SenderUserId = senderUserId,
            MessageText = text,
            IsEdited = false,
            CreatedUtc = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            ModifiedUtc = null,
        };

        await _unitOfWork.ChatMessages.AddAsync(chatMessage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.NotifyChatMessageCreatedAsync(new ChatMessageNotificationDto
        {
            Id = chatMessage.Id,
            InterviewId = chatMessage.InterviewId,
            From = (int)senderType,
            Text = chatMessage.MessageText,
            CreatedUtc = chatMessage.CreatedUtc,
            IsEdited = chatMessage.IsEdited,
            ModifiedUtc = chatMessage.ModifiedUtc
        });

        return (chatMessage.Id, chatMessage.CreatedUtc);
    }

    /// <summary>
    /// Определить тип отправителя сообщения на основе роли пользователя в интервью
    /// </summary>
    private static MessageSenderType DetermineSenderType(Interview interview, AdditionalUserInfo user, bool isAdmin)
    {
        if (isAdmin)
        {
            return MessageSenderType.Admin;
        }

        if (interview.CandidateId == user.Id)
        {
            return MessageSenderType.Candidate;
        }

        if (interview.ExpertId == user.Id)
        {
            return MessageSenderType.Expert;
        }

       return MessageSenderType.Unknown;
    }
}
