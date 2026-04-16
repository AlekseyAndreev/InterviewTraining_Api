using InterviewTraining.Application.Interfaces;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.DatabaseContext;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Services;

/// <summary>
/// Сервис синхронизации пользователей из IdentityServer
/// </summary>
public class UserSyncService(IUnitOfWork _unitOfWork, ILogger<UserSyncService> _logger) : IUserSyncService
{
    public async Task SyncUserAsync(string identityUserId, bool isCandidate, bool isExpert, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Синхронизация пользователя {UserId}: IsCandidate={IsCandidate}, IsExpert={IsExpert}", 
            identityUserId, isCandidate, isExpert);

        var existingUser = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(identityUserId, cancellationToken);

        if (existingUser != null)
        {
            existingUser.IsCandidate = isCandidate;
            existingUser.IsExpert = isExpert;
            existingUser.IsDeleted = false;
            existingUser.ModifiedUtc = DateTime.UtcNow;
            
            _logger.LogInformation("Обновлен пользователь {UserId}", identityUserId);
        }
        else
        {
            var newUser = new AdditionalUserInfo
            {
                Id = Guid.NewGuid(),
                IdentityUserId = identityUserId,
                IsCandidate = isCandidate,
                IsExpert = isExpert,
                IsDeleted = false,
                CreatedUtc = DateTime.UtcNow,
                ModifiedUtc = DateTime.UtcNow,
                TimeZoneId = InterviewContextSeeding.MoscowGuid
            };

            await _unitOfWork.AdditionalUserInfos.AddAsync(newUser);
            
            _logger.LogInformation("Создан новый пользователь {UserId}", identityUserId);
        }

        await _unitOfWork.SaveChangesAsync();
    }
}
