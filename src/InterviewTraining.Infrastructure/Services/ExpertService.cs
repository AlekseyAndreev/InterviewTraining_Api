using InterviewTraining.Application.GetAllExperts.V10;
using InterviewTraining.Application.Interfaces;
using InterviewTraining.Infrastructure.Mappers;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Services;

public class ExpertService(IUnitOfWork unitOfWork) : IExpertService
{
    public async Task<GetAllExpertsResponse> GetByFilterAsync(GetAllExpertsRequest request, CancellationToken cancellationToken)
    {
        var experts = await unitOfWork.AdditionalUserInfos.GetExpertsAsync(cancellationToken);
        return new GetAllExpertsResponse()
        {
            Data = experts.Select(x => x.ToGetExpertResponse()).ToArray()
        };
    }
}
