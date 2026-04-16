using InterviewTraining.Application.GetAllInterviewLanguages.V10;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.Interfaces;

public interface IInterviewLanguageService
{
    public Task<InterviewLanguageResponse[]> GetAllAsync(CancellationToken cancellationToken);
}
