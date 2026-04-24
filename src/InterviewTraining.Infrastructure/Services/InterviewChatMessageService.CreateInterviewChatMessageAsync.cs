using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.CreateInterviewChatMessage.V10;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Domain;
using Microsoft.Extensions.Logging;

namespace InterviewTraining.Infrastructure.Services;

/// <summary>
/// Сервис для работы с интервью
/// </summary>
public partial class InterviewChatMessageService
{
    /// <summary>
    /// Создать сообщение в чате интервью
    /// </summary>
    public async Task<CreateInterviewChatMessageResponse> CreateInterviewChatMessageAsync(CreateInterviewChatMessageRequest request, CancellationToken cancellationToken)
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

        var (interviewChatMessageId, chatCreatedAtUtc) = await interviewChatMessageProvider.CreateInterviewChatMessage(interview.Id, senderType, currentUser.Id, request.MessageText, cancellationToken);

        if (!interviewChatMessageId.HasValue)
        {
            _logger.LogInformation("Сообщение в чате интервью {InterviewId} от пользователя {UserId} не было создано", interview.Id, currentUser.Id);
            throw new BusinessLogicException("Сообщение в чате интервью не было создано");
        }
        else
        {
            _logger.LogInformation("Создано сообщение {MessageId} в чате интервью {InterviewId} от пользователя {UserId}",
                interviewChatMessageId, interview.Id, currentUser.Id);

            return new CreateInterviewChatMessageResponse
            {
                MessageId = interviewChatMessageId.Value,
                CreatedUtc = chatCreatedAtUtc
            };
        }
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
