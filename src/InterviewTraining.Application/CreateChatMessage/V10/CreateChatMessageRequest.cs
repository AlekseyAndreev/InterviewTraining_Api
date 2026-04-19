using InterviewTraining.Application.CustomMediatorLogic;
using System;

namespace InterviewTraining.Application.CreateChatMessage.V10;

/// <summary>
/// Запрос на создание сообщения в чате интервью
/// </summary>
public class CreateChatMessageRequest : IMediatorRequest<CreateChatMessageResponse>
{
    /// <summary>
    /// Идентификатор собеседования
    /// </summary>
    public Guid InterviewId { get; set; }

    /// <summary>
    /// Текст сообщения
    /// </summary>
    public string MessageText { get; set; }

    /// <summary>
    /// Идентификатор текущего пользователя (IdentityUserId)
    /// </summary>
    public string IdentityUserId { get; set; }

    /// <summary>
    /// Является ли пользователь администратором
    /// </summary>
    public bool IsAdmin { get; set; }
}
