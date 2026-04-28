using InterviewTraining.Application.GetAllUsersForAdmin.V10;
using InterviewTraining.Application.GetUserInfo.V10;
using InterviewTraining.Application.UpdateUserInfo.V10;
using InterviewTraining.Application.UpdateUserTimeZone.V10;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.Interfaces;

public interface IUserService
{
    Task<GetUserInfoResponse> GetUserInfoAsync(string identityUserId, CancellationToken cancellationToken);
    Task<UpdateUserInfoResponse> UpdateUserInfoAsync(UpdateUserInfoRequest request, CancellationToken cancellationToken);
    Task<UpdateUserTimeZoneResponse> UpdateUserTimeZoneAsync(UpdateUserTimeZoneRequest request, CancellationToken cancellationToken);
    Task<GetAllUsersForAdminResponse> GetAllUsersForAdminAsync(GetAllUsersForAdminRequest request, CancellationToken cancellationToken);
}
