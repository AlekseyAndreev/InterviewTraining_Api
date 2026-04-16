using InterviewTraining.Domain;
using System;

namespace InterviewTraining.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с версиями интервью
/// </summary>
public interface IInterviewLanguageRepository : IRepository<InterviewLanguage, Guid>
{
}
