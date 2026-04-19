using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.GetSkillsTree.V10;
using InterviewTraining.Application.Interfaces;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

    public async Task<int> UpdateSkillsToCurrentUserAsync(string identityUserId, IEnumerable<Guid> skillIds, CancellationToken cancellationToken)
    {
        // Получаем пользователя по IdentityUserId
        var user = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(identityUserId, cancellationToken);
        if (user == null)
        {
            _logger.LogWarning("Не найден пользователь с IdentityUserId {IdentityUserId}", identityUserId);
            throw new BusinessLogicException("Пользователь не найден");
        }

        await _unitOfWork.UserSkills.DeleteByUserIdAsync(user.Id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

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

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Добавлено {Count} навыков пользователю {UserId}", addedCount, user.Id);
        return addedCount;
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

    public async Task<GetSkillsTreeResponse> GetSkillsTreeAsync(string userId, CancellationToken cancellationToken)
    {
        var allGroups = (await _unitOfWork.SkillGroups.GetFullTreeAsync(cancellationToken))?.ToList() ?? new List<Domain.SkillGroup>();

        if (!allGroups.Any())
        {
            return new GetSkillsTreeResponse { Groups = new List<SkillGroupDto>() };
        }

        var userSkillDtos = await GetUserSelectedSkillIdsAsync(userId, cancellationToken);

        var groupsDict = allGroups.ToDictionary(
            g => g.Id,
            g => new SkillGroupDto
            {
                Id = g.Id,
                Name = g.Name,
                Skills = g.Skills?
                    .Where(s => !s.IsDeleted)
                    .Select(s => new SkillDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        IsSelected = userSkillDtos.Select(x => x.SkillId).Contains(s.Id), // TODO: подумать как можно оптимизировать
                        IsConfirmed = userSkillDtos.FirstOrDefault(t => t.SkillId == s.Id)?.IsConfirmed ?? false, // TODO: подумать как можно оптимизировать
                    })
                    .ToList() ?? new List<SkillDto>(),
                ChildGroups = new List<SkillGroupDto>()
            });

        var rootGroups = allGroups
            .Where(g => g.ParentGroupId == null || g.ParentGroupId == Guid.Empty)
            .Select(g => groupsDict[g.Id])
            .ToList();

        foreach (var group in allGroups.Where(g => g.ParentGroupId.HasValue && g.ParentGroupId != Guid.Empty))
        {
            if (groupsDict.TryGetValue(group.ParentGroupId!.Value, out var parentGroup))
            {
                parentGroup.ChildGroups.Add(groupsDict[group.Id]);
            }
        }

        return new GetSkillsTreeResponse { Groups = rootGroups };
    }

    private record UserSkillDto(Guid SkillId, bool IsConfirmed);

    /// <summary>
    /// Получить идентификаторы выбранных пользователем навыков
    /// </summary>
    private async Task<List<UserSkillDto>> GetUserSelectedSkillIdsAsync(string userId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return new List<UserSkillDto>();
        }

        var user = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(userId, cancellationToken);
        if (user == null)
        {
            _logger.LogDebug("Пользователь с IdentityUserId {UserId} не найден", userId);
            return new List<UserSkillDto>();
        }

        var userSkills = await _unitOfWork.UserSkills.GetByUserIdAsync(user.Id, cancellationToken);
        return userSkills
            .Select(us => new UserSkillDto(us.SkillId, us.IsConfirmed))
            .ToList();
    }
}
