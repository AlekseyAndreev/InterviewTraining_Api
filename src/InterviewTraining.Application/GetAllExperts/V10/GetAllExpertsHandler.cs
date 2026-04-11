using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.GetAllExperts.V10;

public class GetAllExpertsHandler(IExpertService expertService) : IMediatorHandler<GetAllExpertsRequest, GetAllExpertsResponse>
{
    public async Task<GetAllExpertsResponse> HandleAsync(GetAllExpertsRequest request, CancellationToken cancellationToken)
    {
        return await expertService.GetByFilterAsync(request);
    }
}
