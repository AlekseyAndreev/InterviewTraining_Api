using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.GetSkillsTree.V10;

public class GetSkillsTreeRequest : IMediatorRequest<GetSkillsTreeResponse>
{
    public string UserId { get; set; }
}
