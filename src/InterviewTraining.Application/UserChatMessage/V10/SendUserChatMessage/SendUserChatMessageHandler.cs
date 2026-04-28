using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.UserChatMessage.V10.SendUserChatMessage;

public class SendUserChatMessageHandler(IUserChatMessageService service) : IMediatorHandler<SendUserChatMessageRequest, SendUserChatMessageResponse>
{
    public async Task<SendUserChatMessageResponse> HandleAsync(SendUserChatMessageRequest request, CancellationToken cancellationToken)
    {
        return await service.SendMessageAsync(request, cancellationToken);
    }
}
