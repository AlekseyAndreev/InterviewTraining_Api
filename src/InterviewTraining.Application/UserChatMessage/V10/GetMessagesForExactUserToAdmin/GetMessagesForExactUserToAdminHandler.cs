using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.UserChatMessage.V10.GetMessagesForExactUserToAdmin;

public class GetMessagesForExactUserToAdminHandler(IUserChatMessageService service) : IMediatorHandler<GetMessagesForExactUserToAdminRequest, GetMessagesForExactUserToAdminResponse>
{
    public async Task<GetMessagesForExactUserToAdminResponse> HandleAsync(GetMessagesForExactUserToAdminRequest request, CancellationToken cancellationToken)
    {
        return await service.GetMessagesForExactUserToAdminAsync(request, cancellationToken);
    }
}
