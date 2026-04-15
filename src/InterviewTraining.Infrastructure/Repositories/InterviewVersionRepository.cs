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
/// Репозиторий для работы с версиями интервью
/// </summary>
public class InterviewVersionRepository : Repository<InterviewVersion, Guid>, IInterviewVersionRepository
{
    public InterviewVersionRepository(InterviewContext context) : base(context)
    {
    }

    public async Task<IEnumerable<InterviewVersion>> GetByInterviewIdAsync(Guid interviewId)
    {
        return await DbSet
            .Where(v => v.InterviewId == interviewId)
            .OrderByDescending(v => v.CreatedUtc)
            .ToListAsync();
    }

    public override async Task<InterviewVersion> GetByIdAsync(Guid id)
    {
        return await DbSet.FirstOrDefaultAsync(v => v.Id == id);
    }

    public override async Task<IEnumerable<InterviewVersion>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }
}
