using System;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Infrastructure.DatabaseContext;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace InterviewTraining.Infrastructure.Repositories;

/// <summary>
/// Реализация паттерна Unit of Work
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly InterviewContext _context;
    private IDbContextTransaction _transaction;

    private ISkillRepository _skills;
    private ISkillGroupRepository _skillGroups;
    private ISkillTagRepository _skillTags;
    private IUserRatingRepository _userRatings;
    private IAdditionalUserInfoRepository _additionalUserInfos;
    private ITimeZoneRepository _timeZones;
    private IUserSkillRepository _userSkills;

    private bool _disposed;

    /// <summary>
    /// Конструктор
    /// </summary>
    public UnitOfWork(InterviewContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Репозиторий навыков
    /// </summary>
    public ISkillRepository Skills => _skills ??= new SkillRepository(_context);

    /// <summary>
    /// Репозиторий групп навыков
    /// </summary>
    public ISkillGroupRepository SkillGroups => _skillGroups ??= new SkillGroupRepository(_context);

    /// <summary>
    /// Репозиторий тегов навыков
    /// </summary>
    public ISkillTagRepository SkillTags => _skillTags ??= new SkillTagRepository(_context);

    /// <summary>
    /// Репозиторий рейтингов пользователей
    /// </summary>
    public IUserRatingRepository UserRatings => _userRatings ??= new UserRatingRepository(_context);

    /// <summary>
    /// Репозиторий дополнительной информации пользователей
    /// </summary>
    public IAdditionalUserInfoRepository AdditionalUserInfos =>
        _additionalUserInfos ??= new AdditionalUserInfoRepository(_context);

    /// <summary>
    /// Репозиторий часовых поясов
    /// </summary>
    public ITimeZoneRepository TimeZones =>
        _timeZones ??= new TimeZoneRepository(_context);

    /// <summary>
    /// Репозиторий связей пользователей и навыков
    /// </summary>
    public IUserSkillRepository UserSkills =>
        _userSkills ??= new UserSkillRepository(_context);

    /// <summary>
    /// Сохранить все изменения
    /// </summary>
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Сохранить все изменения
    /// </summary>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Начать транзакцию
    /// </summary>
    public async Task BeginTransactionAsync()
    {
        _transaction ??= await _context.Database.BeginTransactionAsync();
    }

    /// <summary>
    /// Зафиксировать транзакцию
    /// </summary>
    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    /// <summary>
    /// Откатить транзакцию
    /// </summary>
    public async Task RollbackTransactionAsync()
    {
        try
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    /// <summary>
    /// Освободить ресурсы
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Освободить ресурсы
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _transaction?.Dispose();
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}
