using System;

namespace InterviewTraining.Domain;

public class InterviewVersion : BaseEntity<Guid>
{
    public Guid InterviewId { get; set; }
    public Interview Interview { get; set; }
    public UserInterviewData Candidate { get; set; }
    public UserInterviewData Expert { get; set; }
    public string LinkToVideoCall { get; set; }
}
