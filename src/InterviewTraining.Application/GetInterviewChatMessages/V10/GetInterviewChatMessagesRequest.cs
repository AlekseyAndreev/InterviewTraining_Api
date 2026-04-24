using InterviewTraining.Application.CustomMediatorLogic;
using System;

namespace InterviewTraining.Application.GetInterviewChatMessages.V10;

/// <summary>
/// Запрос на получение сообщений чата интервью
/// </summary>
public class GetInterviewChatMessagesRequest : IMediatorRequest<GetInterviewChatMessagesResponse>
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