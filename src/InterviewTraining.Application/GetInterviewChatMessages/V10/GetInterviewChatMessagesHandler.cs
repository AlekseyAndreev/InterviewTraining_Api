using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.GetInterviewChatMessages.V10;

/// <summary>
/// Обработчик запроса на получение сообщений чата интервью
/// </summary>
public class GetInterviewChatMessagesHandler(IInterviewChatMessageService service) : IMediatorHandler<GetInterviewChatMessagesRequest, GetInterviewChatMessagesResponse>
{
    public Task<GetInterviewChatMessagesResponse> HandleAsync(GetInterviewChatMessagesRequest request, CancellationToken cancellationToken) =>
        service.GetInterviewChatMessagesAsync(request, cancellationToken);
}