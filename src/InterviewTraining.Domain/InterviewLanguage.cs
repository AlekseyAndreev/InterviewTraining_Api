using System;

namespace InterviewTraining.Domain;

public class InterviewLanguage : BaseDeleteEntity<Guid>
{
    public string Code { get; set; }
    public string NameRu { get; set; }
    public string NameEn { get; set; }
}
