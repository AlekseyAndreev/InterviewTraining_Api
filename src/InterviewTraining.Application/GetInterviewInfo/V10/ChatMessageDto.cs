using System;

namespace InterviewTraining.Application.GetInterviewInfo.V10;

public class ChatMessageDto
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Modified { get; set; }
    public string Text { get; set; }
    public ChatMessageFrom From { get; set; }
    public bool IsEdited { get; set; }
}
