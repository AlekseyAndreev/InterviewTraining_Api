using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.GetInterviewInfo.V10;

/// <summary>
/// Обработчик запроса на получение информации по собеседованию
/// </summary>
public class GetInterviewInfoHandler(IInterviewService interviewService) : IMediatorHandler<GetInterviewInfoRequest, GetInterviewInfoResponse>
{
    public Task<GetInterviewInfoResponse> HandleAsync(GetInterviewInfoRequest request, CancellationToken cancellationToken) =>
        interviewService.GetInterviewInfoAsync(request, cancellationToken);
}
