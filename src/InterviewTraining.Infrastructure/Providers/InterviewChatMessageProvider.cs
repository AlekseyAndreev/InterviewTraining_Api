using InterviewTraining.Application.SignalR;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Providers;

/// <summary>
/// Сервис для работы с интервью
/// </summary>
public class InterviewChatMessageProvider(IUnitOfWork _unitOfWork,
        IInterviewNotificationProvider _notificationProvider) : IInterviewChatMessageProvider
{
    public async Task<(Guid? interviewChatMessageId, DateTime? chatCreatedAtUtc)> CreateInterviewChatMessage(Guid interviewId, MessageSenderType senderType, Guid? senderUserId, string text, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(text))
        {
            return (null, null);
        }

        var interviewChatMessage = new InterviewChatMessage
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

        await _unitOfWork.InterviewChatMessages.AddAsync(interviewChatMessage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationProvider.NotifyChatMessageCreatedAsync(new InterviewChatMessageNotificationDto
        {
            Id = interviewChatMessage.Id,
            InterviewId = interviewChatMessage.InterviewId,
            From = (int)senderType,
            Text = interviewChatMessage.MessageText,
            CreatedUtc = interviewChatMessage.CreatedUtc,
            IsEdited = interviewChatMessage.IsEdited,
            ModifiedUtc = interviewChatMessage.ModifiedUtc
        });

        return (interviewChatMessage.Id, interviewChatMessage.CreatedUtc);
    }
}
