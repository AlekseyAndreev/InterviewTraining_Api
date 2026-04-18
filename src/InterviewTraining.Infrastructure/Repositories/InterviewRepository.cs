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

///<summary>
/// Репозиторий для работы с интервью
/// </summary>
public class InterviewRepository : Repository<Interview, Guid>, IInterviewRepository
{
    public InterviewRepository(InterviewContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Interview>> GetByUserIdAsync(Guid userAdditionalInfoId, CancellationToken cancellationToken)
    {
        return await DbSet
            .Include(i => i.Candidate)
            .Include(i => i.Expert)
            .Include(i => i.ActiveInterviewVersion)
            .Where(i => (i.CandidateId == userAdditionalInfoId || i.ExpertId == userAdditionalInfoId) && !i.Candidate.IsDeleted && !i.Expert.IsDeleted)
            .OrderByDescending(i => i.CreatedUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<Interview> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbSet
            .Include(i => i.Candidate)
                .ThenInclude(c => c.TimeZone)
            .Include(i => i.Expert)
            .Include(i => i.ActiveInterviewVersion)
                .ThenInclude(v => v.Language)
            .Include(i => i.Versions)
            .FirstOrDefaultAsync(i => i.Id == id && !i.Candidate.IsDeleted && !i.Expert.IsDeleted, cancellationToken);
    }

    public override async Task<Interview> GetByIdAsync(Guid id)
    {
        return await DbSet
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public override async Task<IEnumerable<Interview>> GetAllAsync()
    {
        return await DbSet
            .ToListAsync();
    }
}
