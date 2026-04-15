using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.GetMyInterviews.V10;

/// <summary>
/// Обработчик запроса на получение списка интервью пользователя
/// </summary>
public class GetMyInterviewsHandler : IMediatorHandler<GetMyInterviewsRequest, GetMyInterviewsResponse>
{
    private readonly IInterviewService _interviewService;

    public GetMyInterviewsHandler(IInterviewService interviewService)
    {
        _interviewService = interviewService;
    }

    public async Task<GetMyInterviewsResponse> HandleAsync(GetMyInterviewsRequest request, CancellationToken cancellationToken)
    {
        return await _interviewService.GetMyInterviewsAsync(request.IdentityUserId, cancellationToken);
    }
}
