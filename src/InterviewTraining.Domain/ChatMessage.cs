using System;

namespace InterviewTraining.Domain;

/// <summary>
/// Сообщение чата в интервью
/// </summary>
public class ChatMessage : BaseEntity<Guid>
{
    /// <summary>
    /// Идентификатор интервью
    /// </summary>
    public Guid InterviewId { get; set; }
    
    /// <summary>
    /// Навигационное свойство к интервью
    /// </summary>
    public Interview Interview { get; set; }
    
    /// <summary>
    /// Тип отправителя сообщения
    /// </summary>
    public MessageSenderType SenderType { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя-отправителя (null для системных сообщений)
    /// </summary>
    public Guid? SenderUserId { get; set; }

    /// <summary>
    /// Идентификатор пользователя-отправителя (null для системных сообщений)
    /// </summary>
    public AdditionalUserInfo SenderUser { get; set; }

    /// <summary>
    /// Текст сообщения
    /// </summary>
    public string MessageText { get; set; }

    /// <summary>
    /// Признак того, что сообщение было отредактировано
    /// </summary>
    public bool IsEdited { get; set; }
}
