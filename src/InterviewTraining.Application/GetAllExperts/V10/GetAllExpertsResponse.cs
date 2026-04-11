using System.Collections.Generic;

namespace InterviewTraining.Application.GetAllExperts.V10;

public class GetAllExpertsResponse
{
    public IReadOnlyCollection<GetExpertResponse> Experts { get; set; }
}
