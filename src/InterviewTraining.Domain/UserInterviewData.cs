namespace InterviewTraining.Domain;

public class UserInterviewData
{
    public bool IsApproved { get; set; }
    public bool IsPaid { get; set; }
    public bool IsCancelled { get; set; }
    public string CancellReason { get; set; }
}
