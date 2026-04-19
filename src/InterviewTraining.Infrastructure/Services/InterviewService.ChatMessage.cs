using System;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.CreateChatMessage.V10;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.UpdateChatMessage.V10;
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
        // Получаем информацию о текущем пользователе
        var currentUser = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.IdentityUserId, cancellationToken);
        if (currentUser == null)
        {
            _logger.LogWarning("Не найдена информация по пользователю {UserId}", request.IdentityUserId);
            throw new BusinessLogicException("Не найдена информация по пользователю");
        }

        // Получаем интервью
        var interview = await _unitOfWork.Interviews.GetByIdAsync(request.InterviewId);
        if (interview == null)
        {
            _logger.LogWarning("Интервью с идентификатором {InterviewId} не найдено", request.InterviewId);
            throw new EntityNotFoundException("Собеседование не найдено");
        }

        // Проверяем доступ пользователя к интервью
        var senderType = DetermineSenderType(interview, currentUser, request.IsAdmin);
        if (senderType == MessageSenderType.Unknown)
        {
            _logger.LogWarning("Пользователь {UserId} не имеет доступа к интервью {InterviewId}",
                currentUser.Id, interview.Id);
            throw new BusinessLogicException("У вас нет доступа к этому собеседованию");
        }

        // Создаём сообщение
        var chatMessage = new ChatMessage
        {
            InterviewId = request.InterviewId,
            SenderType = senderType,
            SenderUserId = currentUser.Id,
            MessageText = request.MessageText,
            IsEdited = false
        };

        await _unitOfWork.ChatMessages.AddAsync(chatMessage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Создано сообщение {MessageId} в чате интервью {InterviewId} от пользователя {UserId}",
            chatMessage.Id, interview.Id, currentUser.Id);

        return new CreateChatMessageResponse
        {
            MessageId = chatMessage.Id,
            CreatedUtc = chatMessage.CreatedUtc
        };
    }

    /// <summary>
    /// Редактировать сообщение в чате интервью
    /// </summary>
    public async Task<UpdateChatMessageResponse> UpdateChatMessageAsync(UpdateChatMessageRequest request, CancellationToken cancellationToken)
    {
        // Получаем информацию о текущем пользователе
        var currentUser = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.IdentityUserId, cancellationToken);
        if (currentUser == null)
        {
            _logger.LogWarning("Не найдена информация по пользователю {UserId}", request.IdentityUserId);
            throw new BusinessLogicException("Не найдена информация по пользователю");
        }

        // Получаем сообщение с проверкой автора
        var chatMessage = await _unitOfWork.ChatMessages.GetByIdAndSenderAsync(request.MessageId, currentUser.Id);
        if (chatMessage == null)
        {
            _logger.LogWarning("Сообщение {MessageId} не найдено или пользователь {UserId} не является его автором",
                request.MessageId, currentUser.Id);
            throw new EntityNotFoundException("Сообщение не найдено или вы не являетесь его автором");
        }

        // Проверяем, что сообщение принадлежит указанному интервью
        if (chatMessage.InterviewId != request.InterviewId)
        {
            _logger.LogWarning("Сообщение {MessageId} не принадлежит интервью {InterviewId}",
                request.MessageId, request.InterviewId);
            throw new BusinessLogicException("Сообщение не принадлежит указанному собеседованию");
        }

        // Обновляем сообщение
        chatMessage.MessageText = request.MessageText;
        chatMessage.IsEdited = true;
        chatMessage.ModifiedUtc = DateTime.UtcNow;

        _unitOfWork.ChatMessages.Update(chatMessage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Отредактировано сообщение {MessageId} в чате интервью {InterviewId}",
            chatMessage.Id, chatMessage.InterviewId);

        return new UpdateChatMessageResponse
        {
            MessageId = chatMessage.Id,
            ModifiedUtc = chatMessage.ModifiedUtc.Value,
            IsEdited = chatMessage.IsEdited
        };
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
