using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.UserChatMessage.V10.GetUserChatMessagesWithAdmins;

public class GetUserChatMessagesWithAdminsHandler(IUserChatMessageService service) : IMediatorHandler<GetUserChatMessagesWithAdminsRequest, GetUserChatMessagesWithAdminsResponse>
{
    public async Task<GetUserChatMessagesWithAdminsResponse> HandleAsync(GetUserChatMessagesWithAdminsRequest request, CancellationToken cancellationToken)
    {
        return await service.GetMessagesWithAdminsAsync(request, cancellationToken);
    }
}
