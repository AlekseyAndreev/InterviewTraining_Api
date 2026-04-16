using System;
using System.Collections.Generic;

namespace InterviewTraining.Infrastructure.DatabaseContext;

public static partial class InterviewContextSeeding
{
    /// <summary>
    /// Возвращает все языки для собеседования
    /// </summary>
    public static List<Domain.InterviewLanguage> GetInterviewLanguages()
    {
        var interviewLanguages = new List<Domain.InterviewLanguage>
        {
            // EN
            new() { Id = Guid.Parse("20000000-0000-0000-0000-400000000001"), Code = "EN", NameRu = "Английский", NameEn = "English", CreatedUtc = _utc_date, IsDeleted = false },
            // RU
            new() { Id = Guid.Parse("20000000-0000-0000-0000-400000000002"), Code = "RU", NameRu = "Русский", NameEn = "Russian", CreatedUtc = _utc_date, IsDeleted = false },
        };

        return interviewLanguages;
    }
}
