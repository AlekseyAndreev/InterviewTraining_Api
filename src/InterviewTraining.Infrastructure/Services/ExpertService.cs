using InterviewTraining.Application.GetAllExperts.V10;
using InterviewTraining.Application.Interfaces;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Services;

public class ExpertService : IExpertService
{
    public Task<GetAllExpertsResponse> GetByFilterAsync(GetAllExpertsRequest request)
    {
        return Task.FromResult(new GetAllExpertsResponse());
    }
}
