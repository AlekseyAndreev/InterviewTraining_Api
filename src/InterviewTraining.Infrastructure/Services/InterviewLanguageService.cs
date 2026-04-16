using InterviewTraining.Application.GetAllInterviewLanguages.V10;
using InterviewTraining.Application.Interfaces;
using InterviewTraining.Infrastructure.Mappers;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Services;

public class InterviewLanguageService(IUnitOfWork unitOfWork) : IInterviewLanguageService
{
    public async Task<InterviewLanguageResponse[]> GetAllAsync(CancellationToken cancellationToken)
    {
        var all = await unitOfWork.InterviewLanguages.GetAllAsync();
        return all.Select(x => x.ToInterviewLanguageResponse()).ToArray();
    }
}