using System;

namespace InterviewTraining.Application.GetAllExperts.V10;

public class GetExpertResponse
{
    public Guid Id { get; set; }
    public string IdentityServerId { get; set; }
    public string FullName { get; set; }
    public string ShortDescription { get; set; }
}
