using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.CreateInterviewChatMessage.V10;

/// <summary>
/// Обработчик запроса на создание сообщения в чате интервью
/// </summary>
public class CreateInterviewChatMessageHandler(IInterviewChatMessageService service) : IMediatorHandler<CreateInterviewChatMessageRequest, CreateInterviewChatMessageResponse>
{
    public Task<CreateInterviewChatMessageResponse> HandleAsync(CreateInterviewChatMessageRequest request, CancellationToken cancellationToken) =>
        service.CreateInterviewChatMessageAsync(request, cancellationToken);
}