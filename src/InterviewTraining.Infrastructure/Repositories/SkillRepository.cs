using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.DatabaseContext;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterviewTraining.Infrastructure.Repositories;

/// <summary>
/// Репозиторий для работы с навыками
/// </summary>
public class SkillRepository : Repository<Skill, Guid>, ISkillRepository
{
    public SkillRepository(InterviewContext context) : base(context)
    {
    }

    public async Task<Skill> GetByNameAsync(string name)
    {
        return await DbSet
            .FirstOrDefaultAsync(s => s.Name.ToLower() == name.ToLower());
    }

    public async Task<IEnumerable<Skill>> GetByGroupIdAsync(Guid groupId)
    {
        return await DbSet
            .Where(s => s.GroupId == groupId && !s.IsDeleted)
            .ToListAsync();
    }

    public async Task<Skill> GetWithTagsAsync(Guid id)
    {
        return await DbSet
            .Include(s => s.Tags)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Skill> GetWithGroupAsync(Guid id)
    {
        return await DbSet
            .Include(s => s.Group)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Skill> GetWithDetailsAsync(Guid id)
    {
        return await DbSet
            .Include(s => s.Tags)
            .Include(s => s.Group)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Skill>> GetAllWithDetailsAsync()
    {
        return await DbSet
            .Include(s => s.Tags)
            .Include(s => s.Group)
            .Where(s => !s.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<Skill>> SearchAsync(string searchTerm)
    {
        var term = searchTerm.ToLower();
        return await DbSet
            .Include(s => s.Tags)
            .Where(s => !s.IsDeleted &&
                       (s.Name.ToLower().Contains(term) ||
                        s.Tags.Any(t => t.Name.ToLower().Contains(term))))
            .ToListAsync();
    }

    public async Task<IEnumerable<Skill>> GetAllWithGroupsAsync(CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(s => !s.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public override async Task<Skill> GetByIdAsync(Guid id)
    {
        return await DbSet
            .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
    }

    public override async Task<IEnumerable<Skill>> GetAllAsync()
    {
        return await DbSet
            .Where(s => !s.IsDeleted)
            .ToListAsync();
    }
}
