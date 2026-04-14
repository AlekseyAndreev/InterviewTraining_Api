using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;

namespace InterviewTraining.Application.GetSkillsTree.V10;

public class GetSkillsTreeHandler(IUserSkillService userSkillService) : IMediatorHandler<GetSkillsTreeRequest, GetSkillsTreeResponse>
{
    public async Task<GetSkillsTreeResponse> HandleAsync(GetSkillsTreeRequest request, CancellationToken cancellationToken)
    {
        return await userSkillService.GetSkillsTreeAsync(request.UserId, cancellationToken);
    }
}
