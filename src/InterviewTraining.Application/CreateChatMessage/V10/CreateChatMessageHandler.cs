using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.CreateChatMessage.V10;

/// <summary>
/// Обработчик запроса на создание сообщения в чате интервью
/// </summary>
public class CreateChatMessageHandler(IInterviewService interviewService) : IMediatorHandler<CreateChatMessageRequest, CreateChatMessageResponse>
{
    public Task<CreateChatMessageResponse> HandleAsync(CreateChatMessageRequest request, CancellationToken cancellationToken) =>
        interviewService.CreateChatMessageAsync(request, cancellationToken);
}
