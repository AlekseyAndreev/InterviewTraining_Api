namespace InterviewTraining.Domain;

public class CandidateInterviewData : BaseUserInterviewData
{
    /// <summary>
    /// Оплачено ли собеседование кандидатом
    /// </summary>
    public bool IsPaidByCandidate { get; set; }
}
