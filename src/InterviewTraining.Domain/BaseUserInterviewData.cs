namespace InterviewTraining.Domain;

public class BaseUserInterviewData
{
    public bool IsRescheduled { get; set; }
    public bool IsApproved { get; set; }
    public bool IsCancelled { get; set; }
    public string CancelReason { get; set; }
    public bool IsDeleted { get; set; }
}
