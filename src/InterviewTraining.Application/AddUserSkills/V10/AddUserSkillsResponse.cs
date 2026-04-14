namespace InterviewTraining.Application.AddUserSkills.V10;

/// <summary>
/// Ответ на добавление навыков пользователю
/// </summary>
public class AddUserSkillsResponse
{
    /// <summary>
    /// Признак успешного выполнения
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Количество добавленных навыков
    /// </summary>
    public int AddedCount { get; set; }
}
