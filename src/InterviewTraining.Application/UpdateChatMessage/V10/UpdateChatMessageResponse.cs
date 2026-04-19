using System;

namespace InterviewTraining.Application.UpdateChatMessage.V10;

/// <summary>
/// Ответ на редактирование сообщения в чате интервью
/// </summary>
public class UpdateChatMessageResponse
{
    /// <summary>
    /// Идентификатор сообщения
    /// </summary>
    public Guid MessageId { get; set; }

    /// <summary>
    /// Дата и время редактирования сообщения (UTC)
    /// </summary>
    public DateTime ModifiedUtc { get; set; }

    /// <summary>
    /// Признак того, что сообщение было отредактировано
    /// </summary>
    public bool IsEdited { get; set; }
}
