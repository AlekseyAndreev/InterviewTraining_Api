using System;

namespace InterviewTraining.Application.CreateInterviewChatMessage.V10;

/// <summary>
/// Ответ на создание сообщения в чате интервью
/// </summary>
public class CreateInterviewChatMessageResponse
{
    /// <summary>
    /// Идентификатор созданного сообщения
    /// </summary>
    public Guid MessageId { get; set; }

    /// <summary>
    /// Дата и время создания сообщения (UTC)
    /// </summary>
    public DateTime? CreatedUtc { get; set; }
}