using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.UpdateUserSkills.V10;

/// <summary>
/// Обработчик запроса на добавление навыков пользователю
/// </summary>
public class UpdateUserSkillsHandler(IUserSkillService userSkillService) : IMediatorHandler<UpdateUserSkillsRequest, UpdateUserSkillsResponse>
{
    public async Task<UpdateUserSkillsResponse> HandleAsync(UpdateUserSkillsRequest request, CancellationToken cancellationToken)
    {
        var count = await userSkillService.UpdateSkillsToCurrentUserAsync(request.IdentityUserId, request.SkillIds, cancellationToken);
        
        return new UpdateUserSkillsResponse
        {
            Success = true,
            AddedCount = count
        };
    }
}
