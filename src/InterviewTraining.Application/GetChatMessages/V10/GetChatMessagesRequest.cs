using InterviewTraining.Application.CustomMediatorLogic;
using System;

namespace InterviewTraining.Application.GetChatMessages.V10;

/// <summary>
/// Запрос на получение сообщений чата интервью
/// </summary>
public class GetChatMessagesRequest : IMediatorRequest<GetChatMessagesResponse>
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
