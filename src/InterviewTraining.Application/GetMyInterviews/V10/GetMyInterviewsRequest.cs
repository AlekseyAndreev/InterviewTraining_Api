using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.GetMyInterviews.V10;

/// <summary>
/// Запрос на получение списка интервью текущего пользователя
/// </summary>
public class GetMyInterviewsRequest : IMediatorRequest<GetMyInterviewsResponse>
{
    /// <summary>
    /// Идентификатор пользователя из токена
    /// </summary>
    public string IdentityUserId { get; set; }
}
