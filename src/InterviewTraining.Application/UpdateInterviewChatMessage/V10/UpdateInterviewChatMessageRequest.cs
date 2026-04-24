using InterviewTraining.Application.CustomMediatorLogic;
using System;

namespace InterviewTraining.Application.UpdateInterviewChatMessage.V10;

/// <summary>
/// Запрос на редактирование сообщения в чате интервью
/// </summary>
public class UpdateInterviewChatMessageRequest : IMediatorRequest<UpdateInterviewChatMessageResponse>
{
    /// <summary>
    /// Идентификатор собеседования
    /// </summary>
    public Guid InterviewId { get; set; }

    /// <summary>
    /// Идентификатор сообщения
    /// </summary>
    public Guid MessageId { get; set; }

    /// <summary>
    /// Новый текст сообщения
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