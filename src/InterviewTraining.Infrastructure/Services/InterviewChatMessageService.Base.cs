using InterviewTraining.Application.GetInterviewInfo.V10;
using InterviewTraining.Application.Interfaces;
using InterviewTraining.Application.SignalR;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.Providers;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace InterviewTraining.Infrastructure.Services;

/// <summary>
/// Сервис для работы с интервью
/// </summary>
public partial class InterviewChatMessageService(IUnitOfWork _unitOfWork,
        IInterviewChatMessageProvider interviewChatMessageProvider,
        ILogger<InterviewChatMessageService> _logger,
        IInterviewNotificationProvider _notificationProvider) : IInterviewChatMessageService
{
    private static InterviewChatMessageFrom MapMessageSenderType(MessageSenderType messageSenderType) =>
        messageSenderType switch
        {
            MessageSenderType.Admin => InterviewChatMessageFrom.Admin,
            MessageSenderType.Candidate => InterviewChatMessageFrom.Candidate,
            MessageSenderType.Expert => InterviewChatMessageFrom.Expert,
            MessageSenderType.System => InterviewChatMessageFrom.System,
            _ => InterviewChatMessageFrom.Unknown,
        };
}
