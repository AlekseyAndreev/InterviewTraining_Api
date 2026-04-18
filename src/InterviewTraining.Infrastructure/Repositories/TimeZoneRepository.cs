using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Repositories;

/// <summary>
/// Репозиторий для работы с часовыми поясами
/// </summary>
public class TimeZoneRepository : Repository<Domain.TimeZone, Guid>, ITimeZoneRepository
{
    /// <summary>
    /// Конструктор
    /// </summary>
    ///<param name="context">Контекст базы данных</param>
    public TimeZoneRepository(DatabaseContext.InterviewContext context) : base(context)
    {
    }

    /// <inheritdoc />
    public async Task<Domain.TimeZone> GetByCodeAsync(string code)
    {
        return await Context.TimeZones
            .FirstOrDefaultAsync(t => t.Code == code && !t.IsDeleted);
    }

    /// <inheritdoc />
    public new async Task<List<Domain.TimeZone>> GetAllAsync()
    {
        return await Context.TimeZones
            .Where(t => !t.IsDeleted)
            .OrderBy(t => t.Code)
            .ToListAsync();
    }
}
