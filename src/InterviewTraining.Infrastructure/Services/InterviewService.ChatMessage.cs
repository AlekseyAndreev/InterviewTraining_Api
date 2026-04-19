using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.CreateChatMessage.V10;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.GetChatMessages.V10;
using InterviewTraining.Application.GetInterviewInfo.V10;
using InterviewTraining.Application.SignalR;
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

        var chatMessage = new ChatMessage
        {
            InterviewId = request.InterviewId,
            SenderType = senderType,
            SenderUserId = currentUser.Id,
            MessageText = request.MessageText,
            IsEdited = false,
            CreatedUtc = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            ModifiedUtc = null,
        };

        await _unitOfWork.ChatMessages.AddAsync(chatMessage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Отправляем уведомление через SignalR
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

        // Отправляем уведомление через SignalR
        await _notificationService.NotifyChatMessageUpdatedAsync(new ChatMessageNotificationDto
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

   /// <summary>
   /// Получить сообщения чата интервью
   /// </summary>
   public async Task<GetChatMessagesResponse> GetChatMessagesAsync(GetChatMessagesRequest request, CancellationToken cancellationToken)
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

       var hasAccess = request.IsAdmin
           || interview.CandidateId == currentUser.Id
           || interview.ExpertId == currentUser.Id;

       if (!hasAccess)
       {
           _logger.LogWarning("Пользователь {UserId} не имеет доступа к чату интервью {InterviewId}",
               currentUser.Id, interview.Id);
           throw new BusinessLogicException("У вас нет доступа к чату этого собеседования");
       }

       var userTimeZone = currentUser.TimeZoneId.HasValue
           ? await _unitOfWork.TimeZones.GetByIdAsync(currentUser.TimeZoneId.Value)
           : null;
       var timeZoneCode = userTimeZone?.Code;

       var chatMessages = await _unitOfWork.ChatMessages.GetByInterviewIdAsync(request.InterviewId);

       // Маппим сообщения
       var messages = chatMessages.Select(x => new ChatMessageDto
       {
           Id = x.Id,
           From = MapMessageSenderType(x.SenderType),
           Text = x.MessageText,
           Created = ConvertUtcToUserTimeZone(x.CreatedUtc, timeZoneCode),
           IsEdited = x.IsEdited,
           Modified = ConvertUtcToUserTimeZone(x.ModifiedUtc, timeZoneCode)
       }).ToList();

       return new GetChatMessagesResponse
       {
           InterviewId = request.InterviewId,
           Messages = messages
       };
   }
}
