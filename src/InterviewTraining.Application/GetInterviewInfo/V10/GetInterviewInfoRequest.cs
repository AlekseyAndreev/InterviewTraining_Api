using InterviewTraining.Application.CustomMediatorLogic;
using System;

namespace InterviewTraining.Application.GetInterviewInfo.V10;

/// <summary>
/// Запрос на получение информации по собеседованию
/// </summary>
public class GetInterviewInfoRequest : IMediatorRequest<GetInterviewInfoResponse>
{
    /// <summary>
    /// Идентификатор собеседования
    /// </summary>
    public Guid InterviewId { get; set; }

    /// <summary>
    /// Идентификатор текущего пользователя (IdentityUserId)
    /// </summary>
    public string IdentityUserId { get; set; }

    /// <summary>
    /// Является ли пользователь администратором
    /// </summary>
    public bool IsAdmin { get; set; }
}
