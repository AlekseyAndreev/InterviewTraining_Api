using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.GetChatMessages.V10;
using InterviewTraining.Application.GetInterviewInfo.V10;
using Microsoft.Extensions.Logging;

namespace InterviewTraining.Infrastructure.Services;

/// <summary>
/// Сервис для работы с интервью
/// </summary>
public partial class InterviewService
{
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
