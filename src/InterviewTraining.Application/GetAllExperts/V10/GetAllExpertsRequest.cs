using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.GetAllExperts.V10;

public class GetAllExpertsRequest : IMediatorRequest<GetAllExpertsResponse>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
