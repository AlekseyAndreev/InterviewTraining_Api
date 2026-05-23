namespace InterviewTraining.Application.Options;

///<summary>
/// Настройки приложения
///</summary>
public class UiSettings
{
    public const string SectionName = "UiUrls";

    ///<summary>
    /// Разрешённые URL для CORS
    ///</summary>
    public string[] UiUrls { get; set; }
}
