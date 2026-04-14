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
/// Репозиторий для работы со связями пользователей и навыков
/// </summary>
public class UserSkillRepository : Repository<UserSkill, Guid>, IUserSkillRepository
{
    public UserSkillRepository(InterviewContext context) : base(context)
    {
    }

    public async Task<IEnumerable<UserSkill>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(us => us.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<UserSkill> GetByUserAndSkillAsync(Guid userId, Guid skillId, CancellationToken cancellationToken)
    {
        return await DbSet
            .FirstOrDefaultAsync(us => us.UserId == userId && us.SkillId == skillId, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid userId, Guid skillId, CancellationToken cancellationToken)
    {
        return await DbSet
            .AnyAsync(us => us.UserId == userId && us.SkillId == skillId, cancellationToken);
    }

    public async Task DeleteByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var userSkills = await DbSet
            .Where(us => us.UserId == userId)
            .ToListAsync(cancellationToken);
        if (userSkills == null || !userSkills.Any())
        {
            return;
        }

        DbSet.RemoveRange(userSkills);
    }

    public async Task<IEnumerable<UserSkill>> GetByUserIdWithSkillsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await DbSet
            .Include(us => us.Skill)
            .Where(us => us.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public override async Task<UserSkill> GetByIdAsync(Guid id)
    {
        return await DbSet
            .FirstOrDefaultAsync(us => us.Id == id);
    }

    public override async Task<IEnumerable<UserSkill>> GetAllAsync()
    {
        return await DbSet
            .ToListAsync();
    }
}
