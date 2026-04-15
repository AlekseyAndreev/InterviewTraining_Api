using System;
using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.ManageAvailableTime.V10;

///<summary>
/// Запрос на удаление записи доступного времени
/// </summary>
public class DeleteAvailableTimeRequest : IMediatorRequest<DeleteAvailableTimeResponse>
{
    /// <summary>
    /// Идентификатор пользователя из токена
    /// </summary>
    public string IdentityUserId { get; set; }

    /// <summary>
    /// Идентификатор записи для удаления
    /// </summary>
    public Guid Id { get; set; }
}
