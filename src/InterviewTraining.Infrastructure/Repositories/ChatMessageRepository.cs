using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.DatabaseContext;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterviewTraining.Infrastructure.Repositories;

/// <summary>
/// Репозиторий для работы с сообщениями чата
/// </summary>
public class ChatMessageRepository : Repository<ChatMessage, Guid>, IChatMessageRepository
{
    /// <summary>
    /// Конструктор
    /// </summary>
    ///<param name="context">Контекст базы данных</param>
    public ChatMessageRepository(InterviewContext context) : base(context)
    {
    }

    /// <summary>
    /// Получить сообщения по идентификатору интервью
    /// </summary>
    ///<param name="interviewId">Идентификатор интервью</param>
    /// <returns>Список сообщений</returns>
    public async Task<IEnumerable<ChatMessage>> GetByInterviewIdAsync(Guid interviewId)
    {
        return await DbSet
            .Where(x => x.InterviewId == interviewId)
            .OrderByDescending(x => x.CreatedUtc)
            .ToListAsync();
    }

    /// <summary>
    /// Получить сообщения по идентификатору интервью с пагинацией
    /// </summary>
    ///<param name="interviewId">Идентификатор интервью</param>
    ///<param name="skip">Количество пропускаемых сообщений</param>
    /// <param name="take">Количество получаемых сообщений</param>
    /// <returns>Список сообщений</returns>
    public async Task<IEnumerable<ChatMessage>> GetByInterviewIdAsync(Guid interviewId, int skip, int take)
    {
        return await DbSet
            .Where(x => x.InterviewId == interviewId)
            .OrderBy(x => x.CreatedUtc)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    /// <summary>
    /// Получить сообщение с проверкой автора
    /// </summary>
    ///<param name="messageId">Идентификатор сообщения</param>
    /// <param name="senderUserId">Идентификатор пользователя-отправителя</param>
    /// <returns>Сообщение или null</returns>
    public async Task<ChatMessage> GetByIdAndSenderAsync(Guid messageId, Guid senderUserId)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == messageId && x.SenderUserId == senderUserId);
    }
}
