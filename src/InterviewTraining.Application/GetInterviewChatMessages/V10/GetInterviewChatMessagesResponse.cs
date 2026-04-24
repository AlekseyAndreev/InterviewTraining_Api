using System;
using System.Collections.Generic;
using InterviewTraining.Application.GetInterviewInfo.V10;

namespace InterviewTraining.Application.GetInterviewChatMessages.V10;

/// <summary>
/// Ответ на получение сообщений чата интервью
/// </summary>
public class GetInterviewChatMessagesResponse
{
    /// <summary>
    /// Идентификатор собеседования
    /// </summary>
    public Guid InterviewId { get; set; }

    /// <summary>
    /// Список сообщений чата
    /// </summary>
    public List<InterviewChatMessageDto> Messages { get; set; }
}