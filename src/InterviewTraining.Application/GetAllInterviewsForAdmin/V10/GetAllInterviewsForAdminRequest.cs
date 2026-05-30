using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.GetAllInterviewsForAdmin.V10;

/// <summary>
/// Запрос на получение всех интервью для администратора
/// </summary>
public class GetAllInterviewsForAdminRequest : IMediatorRequest<GetAllInterviewsForAdminResponse>
{
    ///<summary>
    /// Идентификатор пользователя из токена
    ///</summary>
    public string IdentityUserId { get; set; }
}
