using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с часовыми поясами
/// </summary>
public interface ITimeZoneRepository : IRepository<Domain.TimeZone, Guid>
{
    /// <summary>
    /// Получить часовой пояс по коду
    /// </summary>
    /// <param name="code">Код часового пояса</param>
    /// <returns>Часовой пояс</returns>
    Task<Domain.TimeZone> GetByCodeAsync(string code);
}
