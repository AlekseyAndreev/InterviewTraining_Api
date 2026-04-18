namespace InterviewTraining.Domain;

public class CandidateInterviewData : BaseUserInterviewData
{
    /// <summary>
    /// Оплачено ли собеседование кандидатом
    /// </summary>
    public bool IsPaidByCandidate { get; set; }

    /// <summary>
    /// Примечания от кандидата при бронировании
    /// </summary>
    public string Notes { get; set; }
}
