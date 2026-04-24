using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.UpdateInterviewChatMessage.V10;

/// <summary>
/// Обработчик запроса на редактирование сообщения в чате интервью
/// </summary>
public class UpdateInterviewChatMessageHandler(IInterviewChatMessageService service) : IMediatorHandler<UpdateInterviewChatMessageRequest, UpdateInterviewChatMessageResponse>
{
    public Task<UpdateInterviewChatMessageResponse> HandleAsync(UpdateInterviewChatMessageRequest request, CancellationToken cancellationToken) =>
        service.UpdateInterviewChatMessageAsync(request, cancellationToken);
}