using InterviewTraining.Application.GetSkillsTree.V10;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.Interfaces;

public interface ISkillService
{
    Task<GetSkillsTreeResponse> GetSkillsTreeAsync(CancellationToken cancellationToken);
}
