using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.GetAllInterviewsForAdmin.V10;

/// <summary>
/// Обработчик запроса на получение всех интервью для администратора
/// </summary>
public class GetAllInterviewsForAdminHandler(IInterviewService _interviewService) : IMediatorHandler<GetAllInterviewsForAdminRequest, GetAllInterviewsForAdminResponse>
{
    public Task<GetAllInterviewsForAdminResponse> HandleAsync(GetAllInterviewsForAdminRequest request, CancellationToken cancellationToken) =>
        _interviewService.GetAllInterviewsForAdminAsync(request.IdentityUserId, cancellationToken);
}
