using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.AddUserSkills.V10;

/// <summary>
/// Обработчик запроса на добавление навыков пользователю
/// </summary>
public class AddUserSkillsHandler(IUserSkillService userSkillService) : IMediatorHandler<AddUserSkillsRequest, AddUserSkillsResponse>
{
    public async Task<AddUserSkillsResponse> HandleAsync(AddUserSkillsRequest request, CancellationToken cancellationToken)
    {
        await userSkillService.AddSkillsToCurrentUserAsync(request.IdentityUserId, request.SkillIds, cancellationToken);
        
        return new AddUserSkillsResponse
        {
            Success = true,
            AddedCount = request.SkillIds is System.Collections.ICollection collection ? collection.Count : 0
        };
    }
}
