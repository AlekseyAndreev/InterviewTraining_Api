using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.UserChatMessage.V10.DeleteUserChatMessage;

public class DeleteUserChatMessageHandler(IUserChatMessageService service) : IMediatorHandler<DeleteUserChatMessageRequest, DeleteUserChatMessageResponse>
{
    public async Task<DeleteUserChatMessageResponse> HandleAsync(DeleteUserChatMessageRequest request, CancellationToken cancellationToken)
    {
        return await service.DeleteMessageAsync(request, cancellationToken);
    }
}
