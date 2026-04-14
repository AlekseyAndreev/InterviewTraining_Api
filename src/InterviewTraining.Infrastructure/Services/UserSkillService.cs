using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.Interfaces;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace InterviewTraining.Infrastructure.Services;

/// <summary>
/// Сервис для работы с навыками пользователей
/// </summary>
public class UserSkillService : IUserSkillService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserSkillService> _logger;

    public UserSkillService(IUnitOfWork unitOfWork, ILogger<UserSkillService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task AddSkillsToCurrentUserAsync(string identityUserId, IEnumerable<Guid> skillIds, CancellationToken cancellationToken)
    {
        // Получаем пользователя по IdentityUserId
        var user = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(identityUserId, cancellationToken);
        if (user == null)
        {
            _logger.LogWarning("Не найден пользователь с IdentityUserId {IdentityUserId}", identityUserId);
            throw new BusinessLogicException("Пользователь не найден");
        }

        var skillIdsList = skillIds.ToList();
        var addedCount = 0;

        foreach (var skillId in skillIdsList)
        {
            // Проверяем существование навыка
            var skill = await _unitOfWork.Skills.GetByIdAsync(skillId);
            if (skill == null)
            {
                _logger.LogWarning("Навык с ID {SkillId} не найден", skillId);
                continue;
            }

            // Проверяем, не добавлен ли уже этот навык пользователю
            var exists = await _unitOfWork.UserSkills.ExistsAsync(user.Id, skillId, cancellationToken);
            if (exists)
            {
                _logger.LogDebug("Навык {SkillId} уже добавлен пользователю {UserId}", skillId, user.Id);
                continue;
            }

            // Создаем связь
            var userSkill = new UserSkill
            {
                UserId = user.Id,
                SkillId = skillId,
                CreatedUtc = DateTime.UtcNow
            };

            await _unitOfWork.UserSkills.AddAsync(userSkill);
            addedCount++;
        }

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Добавлено {Count} навыков пользователю {UserId}", addedCount, user.Id);
    }

    public async Task<IEnumerable<Guid>> GetUserSkillIdsAsync(string identityUserId, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(identityUserId, cancellationToken);
        if (user == null)
        {
            _logger.LogWarning("Не найден пользователь с IdentityUserId {IdentityUserId}", identityUserId);
            throw new BusinessLogicException("Пользователь не найден");
        }

        var userSkills = await _unitOfWork.UserSkills.GetByUserIdAsync(user.Id, cancellationToken);
        return userSkills.Select(us => us.SkillId);
    }
}
