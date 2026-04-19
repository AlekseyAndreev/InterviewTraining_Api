using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.GetChatMessages.V10;

/// <summary>
/// Обработчик запроса на получение сообщений чата интервью
/// </summary>
public class GetChatMessagesHandler(IInterviewService interviewService) : IMediatorHandler<GetChatMessagesRequest, GetChatMessagesResponse>
{
    public Task<GetChatMessagesResponse> HandleAsync(GetChatMessagesRequest request, CancellationToken cancellationToken) =>
        interviewService.GetChatMessagesAsync(request, cancellationToken);
}
