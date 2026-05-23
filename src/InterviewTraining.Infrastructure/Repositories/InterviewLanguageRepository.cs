using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.DatabaseContext;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Repositories;

/// <summary>
/// Репозиторий для работы с версиями интервью
/// </summary>
public class InterviewLanguageRepository : Repository<InterviewLanguage, Guid>, IInterviewLanguageRepository
{
    public InterviewLanguageRepository(InterviewContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<InterviewLanguage>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(u => !u.IsDeleted)
            .ToListAsync(cancellationToken);
    }
}
