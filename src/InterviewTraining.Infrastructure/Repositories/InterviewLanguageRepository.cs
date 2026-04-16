using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.DatabaseContext;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InterviewTraining.Infrastructure.Repositories;

/// <summary>
/// Репозиторий для работы с версиями интервью
/// </summary>
public class InterviewLanguageRepository : Repository<InterviewLanguage, Guid>, IInterviewLanguageRepository
{
    public InterviewLanguageRepository(InterviewContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<InterviewLanguage>> GetAllAsync()
    {
        return await DbSet
            .Where(u => !u.IsDeleted)
            .ToListAsync();
    }
}
