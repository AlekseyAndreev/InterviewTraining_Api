using InterviewTraining.Application.GetAllExperts.V10;
using System.Threading.Tasks;

namespace InterviewTraining.Application.Interfaces;

public interface IExpertService
{
    Task<GetAllExpertsResponse> GetByFilterAsync(GetAllExpertsRequest request);
}
