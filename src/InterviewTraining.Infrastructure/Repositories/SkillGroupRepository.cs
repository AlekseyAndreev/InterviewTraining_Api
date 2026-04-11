using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.DatabaseContext;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterviewTraining.Infrastructure.Repositories;

/// <summary>
/// Репозиторий для работы с группами навыков
/// </summary>
public class SkillGroupRepository : Repository<SkillGroup, Guid>, ISkillGroupRepository
{
    public SkillGroupRepository(InterviewContext context) : base(context)
    {
    }

    public async Task<SkillGroup> GetByNameAsync(string name)
    {
        return await DbSet
            .FirstOrDefaultAsync(g => g.Name.ToLower() == name.ToLower());
    }

    public async Task<SkillGroup> GetWithSkillsAsync(Guid id)
    {
        return await DbSet
            .Include(g => g.Skills)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<IEnumerable<SkillGroup>> GetRootGroupsAsync()
    {
        return await DbSet
            .Where(g => g.ParentGroupId == null && !g.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<SkillGroup>> GetChildGroupsAsync(Guid parentGroupId)
    {
        return await DbSet
            .Where(g => g.ParentGroupId == parentGroupId && !g.IsDeleted)
            .ToListAsync();
    }

    public async Task<SkillGroup> GetWithChildGroupsAsync(Guid id)
    {
        return await DbSet
            .Include(g => g.ChildGroups)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<SkillGroup> GetFullHierarchyAsync(Guid id)
    {
        return await DbSet
            .Include(g => g.Skills)
            .Include(g => g.ChildGroups)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public override async Task<SkillGroup> GetByIdAsync(Guid id)
    {
        return await DbSet
            .FirstOrDefaultAsync(g => g.Id == id && !g.IsDeleted);
    }

    public override async Task<IEnumerable<SkillGroup>> GetAllAsync()
    {
        return await DbSet
            .Where(g => !g.IsDeleted)
            .ToListAsync();
    }
}
