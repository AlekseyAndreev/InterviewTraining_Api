using System;
using System.Collections.Generic;
using InterviewTraining.Application.GetInterviewInfo.V10;

namespace InterviewTraining.Application.GetChatMessages.V10;

/// <summary>
/// Ответ на получение сообщений чата интервью
/// </summary>
public class GetChatMessagesResponse
{
    /// <summary>
    /// Идентификатор собеседования
    /// </summary>
    public Guid InterviewId { get; set; }

    /// <summary>
    /// Список сообщений чата
    /// </summary>
    public List<ChatMessageDto> Messages { get; set; }
}
