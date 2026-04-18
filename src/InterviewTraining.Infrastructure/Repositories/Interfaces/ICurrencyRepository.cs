using InterviewTraining.Domain;
using System;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с валютами
/// </summary>
public interface ICurrencyRepository : IRepository<Currency, Guid>
{
    /// <summary>
    /// Получить валюту по коду
    /// </summary>
    ///<param name="code">Код валюты (ISO 4217)</param>
    /// <returns>Валюта или null</returns>
    Task<Currency> GetByCodeAsync(string code);
}
