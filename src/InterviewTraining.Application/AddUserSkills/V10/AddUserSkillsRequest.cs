using System;
using System.Collections.Generic;
using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.AddUserSkills.V10;

/// <summary>
/// Запрос на добавление навыков пользователю
/// </summary>
public class AddUserSkillsRequest : IMediatorRequest<AddUserSkillsResponse>
{
    /// <summary>
    /// Идентификатор пользователя из токена
    /// </summary>
    public string IdentityUserId { get; set; }

    /// <summary>
    /// Массив идентификаторов навыков для добавления
    /// </summary>
    public IEnumerable<Guid> SkillIds { get; set; }
}
