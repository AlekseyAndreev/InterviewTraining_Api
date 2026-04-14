using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;

namespace InterviewTraining.Application.GetSkillsTree.V10;

public class GetSkillsTreeHandler(ISkillService skillService) : IMediatorHandler<GetSkillsTreeRequest, GetSkillsTreeResponse>
{
    public async Task<GetSkillsTreeResponse> HandleAsync(GetSkillsTreeRequest request, CancellationToken cancellationToken)
    {
        return await skillService.GetSkillsTreeAsync(cancellationToken);
    }
}
