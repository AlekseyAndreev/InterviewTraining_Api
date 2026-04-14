using InterviewTraining.Application.GetUserInfo.V10;
using InterviewTraining.Application.UpdateUserInfo.V10;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.Interfaces;

public interface IUserService
{
    Task<GetUserInfoResponse> GetUserInfoAsync(string identityUserId, CancellationToken cancellationToken);
    Task<UpdateUserInfoResponse> UpdateUserInfoAsync(UpdateUserInfoRequest request, CancellationToken cancellationToken);
}
