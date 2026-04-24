using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InterviewTraining.Domain;

namespace InterviewTraining.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с сообщениями чата
/// </summary>
public interface IInterviewChatMessageRepository : IRepository<InterviewChatMessage, Guid>
{
    /// <summary>
    /// Получить сообщения по идентификатору интервью
    /// </summary>
    ///<param name="interviewId">Идентификатор интервью</param>
    /// <returns>Список сообщений</returns>
    Task<IEnumerable<InterviewChatMessage>> GetByInterviewIdAsync(Guid interviewId);

    /// <summary>
    /// Получить сообщения по идентификатору интервью с пагинацией
    /// </summary>
    ///<param name="interviewId">Идентификатор интервью</param>
    ///<param name="skip">Количество пропускаемых сообщений</param>
    /// <param name="take">Количество получаемых сообщений</param>
    /// <returns>Список сообщений</returns>
    Task<IEnumerable<InterviewChatMessage>> GetByInterviewIdAsync(Guid interviewId, int skip, int take);

    /// <summary>
    /// Получить сообщение с проверкой автора
    /// </summary>
    ///<param name="messageId">Идентификатор сообщения</param>
    ///<param name="senderUserId">Идентификатор пользователя-отправителя</param>
    /// <returns>Сообщение или null</returns>
    Task<InterviewChatMessage> GetByIdAndSenderAsync(Guid messageId, Guid senderUserId);
}
