using System;

namespace InterviewTraining.Domain;

public class InterviewVersion : BaseEntity<Guid>
{
    public Guid InterviewId { get; set; }
    public Interview Interview { get; set; }
    public CandidateInterviewData Candidate { get; set; }
    public ExpertInterviewData Expert { get; set; }
    public bool IsAdminApproved { get; set; }
    public string LinkToVideoCall { get; set; }
    public DateTime StartUtc { get; set; }
    public DateTime? EndUtc { get; set; }
    public Guid? LanguageId { get; set; }
    public InterviewLanguage Language { get; set; }
}
