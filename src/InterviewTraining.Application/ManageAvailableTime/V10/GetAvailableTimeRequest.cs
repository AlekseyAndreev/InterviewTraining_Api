using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.ManageAvailableTime.V10;

/// <summary>
/// Запрос на получение списка доступного времени пользователя
/// </summary>
public class GetAvailableTimeRequest : IMediatorRequest<GetAvailableTimeResponse>
{
    /// <summary>
    /// Идентификатор пользователя из токена
    /// </summary>
    public string IdentityUserId { get; set; }
}
