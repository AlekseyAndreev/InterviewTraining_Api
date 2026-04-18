using System.Linq;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.GetUserInfo.V10;
using InterviewTraining.Application.Interfaces;
using InterviewTraining.Application.UpdateUserInfo.V10;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.UpdateUserTimeZone.V10;

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

        var timeZones = await unitOfWork.TimeZones.GetAllAsync();

        return new GetUserInfoResponse
        {
            Description = userInfo.Description,
            FullName = userInfo.FullName,
            ShortDescription = userInfo.ShortDescription,
            Photo = userInfo.PhotoLocal,
            InterviewPrice = userInfo.InterviewPrice,
            SelectedCurrencyId = userInfo.CurrencyId,
            SelectedTimeZoneId = userInfo.TimeZoneId,
            TimeZones = timeZones
                .Where(tz => !tz.IsDeleted)
                .Select(tz => new TimeZoneDto
                {
                    Id = tz.Id,
                    Code = tz.Code,
                    Description = tz.Description
                })
                .ToList()
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

        userInfo.PhotoLocal = request.Photo;
        userInfo.FullName = request.FullName;
        userInfo.ShortDescription = request.ShortDescription;
        userInfo.Description = request.Description;

        if (request.InterviewPrice.HasValue && request.IsExpert)
        {
            if (!request.CurrencyId.HasValue)
            {
                logger.LogWarning("Не указана валюта для пользователя {UserId}", request.IdentityUserId);
                throw new BusinessLogicException("При указании суммы должна быть выбрана валюта");
            }

            var currency = await unitOfWork.Currencies.GetByIdAsync(request.CurrencyId.Value);
            if (currency == null)
            {
                logger.LogWarning("Валюта {CurrencyId} не найдена", request.CurrencyId);
                throw new BusinessLogicException("Указанная валюта не найдена");
            }

            userInfo.InterviewPrice = request.InterviewPrice;
            userInfo.CurrencyId = request.CurrencyId;
        }
        else
        {
            userInfo.InterviewPrice = null;
            userInfo.CurrencyId = null;
        }

        await unitOfWork.SaveChangesAsync();

        logger.LogInformation("Обновлена информация пользователя {UserId}", request.IdentityUserId);

        return new UpdateUserInfoResponse { Success = true };
    }

    public async Task<UpdateUserTimeZoneResponse> UpdateUserTimeZoneAsync(UpdateUserTimeZoneRequest request, CancellationToken cancellationToken)
    {
        var userInfo = await unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.IdentityUserId, cancellationToken);
        if (userInfo == null)
        {
            logger.LogWarning("Не найдена информация по пользователю {UserId}", request.IdentityUserId);
            throw new BusinessLogicException("Не найдена информация по пользователю");
        }

        if (request.TimeZoneId.HasValue)
        {
            var timeZone = await unitOfWork.TimeZones.GetByIdAsync(request.TimeZoneId.Value);
            if (timeZone == null || timeZone.IsDeleted)
            {
                throw new BusinessLogicException("Указанная временная зона не найдена");
            }

            userInfo.TimeZoneId = request.TimeZoneId;
        }
        else
        {
            userInfo.TimeZoneId = null;
        }

        await unitOfWork.SaveChangesAsync();

        logger.LogInformation("Обновлена информация пользователя {UserId}", request.IdentityUserId);

        return new UpdateUserTimeZoneResponse { Success = true };
    }
}
