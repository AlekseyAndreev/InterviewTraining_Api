using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.UpdateChatMessage.V10;

/// <summary>
/// Обработчик запроса на редактирование сообщения в чате интервью
/// </summary>
public class UpdateChatMessageHandler(IInterviewService interviewService) : IMediatorHandler<UpdateChatMessageRequest, UpdateChatMessageResponse>
{
    public Task<UpdateChatMessageResponse> HandleAsync(UpdateChatMessageRequest request, CancellationToken cancellationToken) =>
        interviewService.UpdateChatMessageAsync(request, cancellationToken);
}
