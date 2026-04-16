using System;

namespace InterviewTraining.Application.GetAllInterviewLanguages.V10;

public class InterviewLanguageResponse
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string NameRu { get; set; }
    public string NameEn { get; set; }
}
