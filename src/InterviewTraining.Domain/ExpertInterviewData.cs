namespace InterviewTraining.Domain;

public class ExpertInterviewData : BaseUserInterviewData
{
    /// <summary>
    /// Оплачено ли собеседование эксперту
    /// </summary>
    public bool IsPaidToExpert { get; set; }
}
