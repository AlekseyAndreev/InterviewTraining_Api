using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.UserChatMessage.V10.MarkUserChatMessageAsRead;

public class MarkUserChatMessagesAsReadHandler(IUserChatMessageService service) : IMediatorHandler<MarkUserChatMessageAsReadRequest, MarkUserChatMessageAsReadResponse>
{
    public async Task<MarkUserChatMessageAsReadResponse> HandleAsync(MarkUserChatMessageAsReadRequest request, CancellationToken cancellationToken)
    {
        return await service.MarkMessageAsReadAsync(request, cancellationToken);
    }
}
