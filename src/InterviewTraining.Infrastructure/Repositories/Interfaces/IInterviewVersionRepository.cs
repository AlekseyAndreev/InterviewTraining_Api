using InterviewTraining.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с версиями интервью
/// </summary>
public interface IInterviewVersionRepository : IRepository<InterviewVersion, Guid>
{
    /// <summary>
    /// Получить все версии для интервью
    /// </summary>
    Task<IEnumerable<InterviewVersion>> GetByInterviewIdAsync(Guid interviewId);
}
