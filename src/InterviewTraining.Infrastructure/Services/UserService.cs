using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.GetUserInfo.V10;
using InterviewTraining.Application.Interfaces;
using InterviewTraining.Application.UpdateUserInfo.V10;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Services;

public class UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger) : IUserService
{
    public async Task<GetUserInfoResponse> GetUserInfoAsync(string identityUserId, CancellationToken cancellationToken)
    {
        var userInfo = await unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(identityUserId, cancellationToken);
        if (userInfo == null)
        {
            logger.LogWarning("Не найдена информация по пользователю {UserId}", identityUserId);
            throw new BusinessLogicException("Не найдена информация по пользователю");
        }

        return new GetUserInfoResponse
        {
            Description = userInfo.Description,
            FullName = userInfo.FullName,
            ShortDescription = userInfo.ShortDescription,
            Photo = userInfo.PhotoLocal,
            PhotoUrl = userInfo.PhotoUrl
        };
    }

    public async Task<UpdateUserInfoResponse> UpdateUserInfoAsync(UpdateUserInfoRequest request, CancellationToken cancellationToken)
    {
        var userInfo = await unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.IdentityUserId, cancellationToken);
        if (userInfo == null)
        {
            logger.LogWarning("Не найдена информация по пользователю {UserId}", request.IdentityUserId);
            throw new BusinessLogicException("Не найдена информация по пользователю");
        }

        userInfo.PhotoUrl = request.PhotoUrl;
        userInfo.PhotoLocal = request.Photo;
        userInfo.FullName = request.FullName;
        userInfo.ShortDescription = request.ShortDescription;
        userInfo.Description = request.Description;

        await unitOfWork.SaveChangesAsync();

        logger.LogInformation("Обновлена информация пользователя {UserId}", request.IdentityUserId);

        return new UpdateUserInfoResponse { Success = true };
    }
}
