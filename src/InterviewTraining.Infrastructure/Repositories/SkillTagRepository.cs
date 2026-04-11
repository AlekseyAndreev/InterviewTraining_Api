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
/// Репозиторий для работы с тегами навыков
/// </summary>
public class SkillTagRepository : Repository<SkillTag, Guid>, ISkillTagRepository
{
    public SkillTagRepository(InterviewContext context) : base(context)
    {
    }

    public async Task<SkillTag> GetByNameAsync(string name)
    {
        return await DbSet
            .FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower());
    }

    public async Task<IEnumerable<SkillTag>> GetBySkillIdAsync(Guid skillId)
    {
        return await DbSet
            .Where(t => t.SkillId == skillId && !t.IsDeleted)
            .ToListAsync();
    }

    public async Task<SkillTag> GetWithSkillAsync(Guid id)
    {
        return await DbSet
            .Include(t => t.Skill)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<SkillTag>> SearchAsync(string searchTerm)
    {
        var term = searchTerm.ToLower();
        return await DbSet
            .Include(t => t.Skill)
            .Where(t => !t.IsDeleted && t.Name.ToLower().Contains(term))
            .ToListAsync();
    }

    public async Task DeleteBySkillIdAsync(Guid skillId)
    {
        var tags = await DbSet
            .Where(t => t.SkillId == skillId)
            .ToListAsync();

        DbSet.RemoveRange(tags);
    }

    public override async Task<SkillTag> GetByIdAsync(Guid id)
    {
        return await DbSet
            .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
    }

    public override async Task<IEnumerable<SkillTag>> GetAllAsync()
    {
        return await DbSet
            .Where(t => !t.IsDeleted)
            .ToListAsync();
    }
}
