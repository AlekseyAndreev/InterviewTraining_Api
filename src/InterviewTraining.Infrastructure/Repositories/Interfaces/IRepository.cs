using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Базовый интерфейс репозитория для работы с сущностями
/// </summary>
/// <typeparam name="T">Тип сущности</typeparam>
///<typeparam name="TKey">Тип идентификатора</typeparam>
public interface IRepository<T, TKey> where T : class
{
    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    Task<T> GetByIdAsync(TKey id);

    /// <summary>
    /// Получить все сущности
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Найти сущности по условию
    /// </summary>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Найти первую сущность по условию
    /// </summary>
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Добавить сущность
    /// </summary>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Добавить несколько сущностей
    /// </summary>
    Task AddRangeAsync(IEnumerable<T> entities);

    /// <summary>
    /// Обновить сущность
    /// </summary>
    void Update(T entity);

    /// <summary>
    /// Удалить сущность
    /// </summary>
    void Delete(T entity);

    /// <summary>
    /// Удалить несколько сущностей
    /// </summary>
    void DeleteRange(IEnumerable<T> entities);

    /// <summary>
    /// Проверить существование сущности по условию
    /// </summary>
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Получить количество сущностей
    /// </summary>
    Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
}
