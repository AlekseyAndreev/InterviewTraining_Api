using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.GetSkillsTree.V10;
using InterviewTraining.Application.Interfaces;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace InterviewTraining.Infrastructure.Services;

/// <summary>
/// Сервис для работы с навыками
/// </summary>
public class SkillService : ISkillService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMemoryCache _cache;
    private readonly ILogger<SkillService> _logger;
    private const string CacheKey = "SkillsTree";
    private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(3);

    public SkillService(IUnitOfWork unitOfWork, IMemoryCache cache, ILogger<SkillService> logger)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
        _logger = logger;
    }

    public async Task<GetSkillsTreeResponse> GetSkillsTreeAsync(CancellationToken cancellationToken)
    {
        if (_cache.TryGetValue(CacheKey, out GetSkillsTreeResponse cachedResponse))
        {
            _logger.LogInformation("Данные о дереве навыков получены из кэша");
            return cachedResponse;
        }

        _logger.LogInformation("Данные о дереве навыков отсутствуют в кэше, загружаем из БД");

        var response = await LoadFromDatabaseAsync(cancellationToken);

        _cache.Set(CacheKey, response, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = CacheDuration
        });

        _logger.LogInformation("Данные о дереве навыков сохранены в кэш на {Hours} часов", CacheDuration.TotalHours);

        return response;
    }

    private async Task<GetSkillsTreeResponse> LoadFromDatabaseAsync(CancellationToken cancellationToken)
    {
        var allGroups = (await _unitOfWork.SkillGroups.GetFullTreeAsync(cancellationToken))?.ToList() ?? new List<Domain.SkillGroup>();

        if (!allGroups.Any())
        {
            return new GetSkillsTreeResponse { Groups = new List<SkillGroupDto>() };
        }

        var groupsDict = allGroups.ToDictionary(
            g => g.Id,
            g => new SkillGroupDto
            {
                Id = g.Id,
                Name = g.Name,
                Skills = g.Skills?
                    .Where(s => !s.IsDeleted)
                    .Select(s => new SkillDto { Id = s.Id, Name = s.Name })
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
}
