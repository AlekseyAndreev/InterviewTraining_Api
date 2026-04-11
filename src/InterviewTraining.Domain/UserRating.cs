using System;

namespace InterviewTraining.Domain;

/// <summary>
/// Рейтинг пользователя от другого пользователя
/// </summary>
public class UserRating : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// Идентификатор пользователя, который поставил рейтинг
    /// </summary>
    public Guid UserFromId { get; set; }

    /// <summary>
    /// Навигационное свойство к пользователю, который поставил рейтинг
    /// </summary>
    public AdditionalUserInfo UserFrom { get; set; }

    /// <summary>
    /// Идентификатор пользователя, которому поставили рейтинг
    /// </summary>
    public Guid UserToId { get; set; }

    /// <summary>
    /// Навигационное свойство к пользователю, которому поставили рейтинг
    /// </summary>
    public AdditionalUserInfo UserTo { get; set; }

    /// <summary>
    /// Значение рейтинга (от 1 до 5)
    /// </summary>
    public int RatingValue { get; set; }

    /// <summary>
    /// Комментарий к рейтингу
    /// </summary>
    public string Comment { get; set; }
}
