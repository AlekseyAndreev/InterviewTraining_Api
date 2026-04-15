using System;
using System.Collections.Generic;

namespace InterviewTraining.Domain;

public class Interview : BaseEntity<Guid>
{
    public Guid CandidateId { get; set; }
    public AdditionalUserInfo Candidate { get; set; }
    public Guid ExpertId { get; set; }
    public AdditionalUserInfo Expert { get; set; }
    public Guid? ActiveInterviewVersionId { get; set; }
    public InterviewVersion ActiveInterviewVersion { get; set; }
    public List<InterviewVersion> Versions { get; set; }
}

