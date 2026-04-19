namespace InterviewTraining.Domain;

/// <summary>
/// Тип отправителя сообщения
/// </summary>
public enum MessageSenderType
{
    /// <summary>
    /// Unknown
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Кандидат
    /// </summary>
    Candidate = 1,
    
    /// <summary>
    /// Эксперт
    /// </summary>
    Expert = 2,
    
    /// <summary>
    /// Администратор
    /// </summary>
    Admin = 3,
    
    /// <summary>
    /// Система
    /// </summary>
    System = 4
}
