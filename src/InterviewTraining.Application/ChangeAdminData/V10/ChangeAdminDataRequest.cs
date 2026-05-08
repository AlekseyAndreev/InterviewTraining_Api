using InterviewTraining.Application.CustomMediatorLogic;
using System;

namespace InterviewTraining.Application.ChangeAdminData.V10;

/// <summary>
/// Изменить данные для собеседования от админа
/// </summary>
public class ChangeAdminDataRequest : IMediatorRequest<ChangeAdminDataResponse>
{
    public string CurrentIdentityUserId { get; set; }
    public bool IsAdmin { get; set; }
    public Guid InterviewId { get; set; }
    public string LinkToVideoCall { get; set; }
    public bool? IsPaidByCandidate { get; set; }
    public bool IsPaidToExpert { get; set; }
}
