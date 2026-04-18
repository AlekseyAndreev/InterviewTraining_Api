namespace InterviewTraining.Application.GetInterviewInfo.V10;

/// <summary>
/// DTO данных подтверждения участника
/// </summary>
public class ParticipantApprovalDto
{
    /// <summary>
    /// Был ли перенос времени
    /// </summary>
    public bool IsRescheduled { get; set; }

    /// <summary>
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
