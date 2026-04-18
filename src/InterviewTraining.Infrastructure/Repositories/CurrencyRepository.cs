using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Repositories;

/// <summary>
/// Репозиторий для работы с валютами
/// </summary>
public class CurrencyRepository : Repository<Currency, Guid>, ICurrencyRepository
{
    /// <summary>
    /// Конструктор
    /// </summary>
    ///<param name="context">Контекст базы данных</param>
    public CurrencyRepository(DatabaseContext.InterviewContext context) : base(context)
    {
    }

    /// <inheritdoc />
    public async Task<Currency> GetByCodeAsync(string code)
    {
        return await Context.Currencies
            .FirstOrDefaultAsync(c => c.Code == code);
    }
}
