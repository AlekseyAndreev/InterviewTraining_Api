using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.GetAllInterviewLanguages.V10;

public class GetAllInterviewLanguagesHandler(IInterviewLanguageService service) : IMediatorHandler<GetAllInterviewLanguagesRequest, InterviewLanguageResponse[]>
{
    public async Task<InterviewLanguageResponse[]> HandleAsync(GetAllInterviewLanguagesRequest request, CancellationToken cancellationToken)
    {
        return await service.GetAllAsync(cancellationToken);
    }
}
