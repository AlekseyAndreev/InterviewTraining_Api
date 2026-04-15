namespace InterviewTraining.Domain;

public class BaseUserInterviewData
{
    public bool IsApproved { get; set; }
    public bool IsPaid { get; set; }
    public bool IsCancelled { get; set; }
    public string CancellReason { get; set; }
}
