using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.UserChatMessage.V10.EditUserChatMessage;

public class EditUserChatMessageHandler(IUserChatMessageService service) : IMediatorHandler<EditUserChatMessageRequest, EditUserChatMessageResponse>
{
    public async Task<EditUserChatMessageResponse> HandleAsync(EditUserChatMessageRequest request, CancellationToken cancellationToken)
    {
        return await service.EditMessageAsync(request, cancellationToken);
    }
}
