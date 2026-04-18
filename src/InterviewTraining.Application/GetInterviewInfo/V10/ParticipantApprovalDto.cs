namespace InterviewTraining.Application.GetInterviewInfo.V10;

/// <summary>
/// DTO данных подтверждения участника
/// </summary>
public class ParticipantApprovalDto
{
    ///<summary>
    /// Подтверждено ли участие
    /// </summary>
    public bool IsApproved { get; set; }

    /// <summary>
    /// Отменено ли
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Причина отмены
    /// </summary>
    public string CancelReason { get; set; }
}
