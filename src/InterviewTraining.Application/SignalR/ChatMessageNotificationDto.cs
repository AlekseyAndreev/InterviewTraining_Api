using System;

namespace InterviewTraining.Application.SignalR;

/// <summary>
/// DTO для уведомления о сообщении чата через SignalR
/// </summary>
public class ChatMessageNotificationDto
{
    /// <summary>
    /// Идентификатор сообщения
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор собеседования
    /// </summary>
    public Guid InterviewId { get; set; }

    /// <summary>
    /// Тип отправителя
    /// </summary>
    public int From { get; set; }

    /// <summary>
    /// Текст сообщения
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Дата и время создания (UTC)
    /// </summary>
    public DateTime CreatedUtc { get; set; }

    /// <summary>
    /// Признак того, что сообщение было отредактировано
    /// </summary>
    public bool IsEdited { get; set; }

    /// <summary>
    /// Дата и время редактирования (UTC)
    /// </summary>
    public DateTime? ModifiedUtc { get; set; }
}
