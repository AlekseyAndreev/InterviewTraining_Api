using InterviewTraining.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Providers;

public interface IInterviewChatMessageProvider
{
    Task<(Guid? interviewChatMessageId, DateTime? chatCreatedAtUtc)> CreateInterviewChatMessage(Guid interviewId, MessageSenderType senderType, Guid? senderUserId, string text, CancellationToken cancellationToken);
}
