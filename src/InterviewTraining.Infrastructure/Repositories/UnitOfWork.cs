using System;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Infrastructure.DatabaseContext;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace InterviewTraining.Infrastructure.Repositories;

///<summary>
/// Unit of Work implementation
///</summary>
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
    private IUserAvailableTimeRepository _userAvailableTimes;
    private IInterviewRepository _interviews;
    private IInterviewVersionRepository _interviewVersions;
    private IInterviewLanguageRepository _interviewLanguages;
    private ICurrencyRepository _currencies;
    private IInterviewChatMessageRepository _interviewChatMessages;
    private IUserNotificationRepository _userNotifications;
    private IUserChatMessageRepository _userChatMessages;

    private bool _disposed;

    ///<summary>
    /// Constructor
    ///</summary>
    public UnitOfWork(InterviewContext context)
    {
        _context = context;
    }

    ///<summary>
    /// Skills repository
    ///</summary>
    public ISkillRepository Skills => _skills ??= new SkillRepository(_context);

    ///<summary>
    /// Skill groups repository
    ///</summary>
    public ISkillGroupRepository SkillGroups => _skillGroups ??= new SkillGroupRepository(_context);

    ///<summary>
    /// Skill tags repository
    ///</summary>
    public ISkillTagRepository SkillTags => _skillTags ??= new SkillTagRepository(_context);

    ///<summary>
    /// User ratings repository
    ///</summary>
    public IUserRatingRepository UserRatings => _userRatings ??= new UserRatingRepository(_context);

    ///<summary>
    /// Additional user info repository
    ///</summary>
    public IAdditionalUserInfoRepository AdditionalUserInfos =>
        _additionalUserInfos ??= new AdditionalUserInfoRepository(_context);

    ///<summary>
    /// Time zones repository
    ///</summary>
    public ITimeZoneRepository TimeZones =>
        _timeZones ??= new TimeZoneRepository(_context);

    ///<summary>
    /// User skills repository
    ///</summary>
    public IUserSkillRepository UserSkills =>
        _userSkills ??= new UserSkillRepository(_context);

    ///<summary>
    /// User available times repository
    ///</summary>
    public IUserAvailableTimeRepository UserAvailableTimes =>
        _userAvailableTimes ??= new UserAvailableTimeRepository(_context);

    ///<summary>
    /// Interviews repository
    ///</summary>
    public IInterviewRepository Interviews =>
        _interviews ??= new InterviewRepository(_context);

    ///<summary>
    /// Interview versions repository
    ///</summary>
    public IInterviewVersionRepository InterviewVersions =>
        _interviewVersions ??= new InterviewVersionRepository(_context);

    ///<summary>
    /// Interview languages repository
    ///</summary>
    public IInterviewLanguageRepository InterviewLanguages =>
        _interviewLanguages ??= new InterviewLanguageRepository(_context);

    ///<summary>
    /// Currencies repository
    ///</summary>
    public ICurrencyRepository Currencies =>
        _currencies ??= new CurrencyRepository(_context);

    ///<summary>
    /// Interview chat messages repository
    ///</summary>
    public IInterviewChatMessageRepository InterviewChatMessages =>
        _interviewChatMessages ??= new InterviewChatMessageRepository(_context);

    ///<summary>
    /// User notifications repository
    ///</summary>
    public IUserNotificationRepository UserNotifications =>
        _userNotifications ??= new UserNotificationRepository(_context);

    ///<summary>
    /// User chat messages repository
    ///</summary>
    public IUserChatMessageRepository UserChatMessages =>
        _userChatMessages ??= new UserChatMessageRepository(_context);

    ///<summary>
    /// Save all changes
    ///</summary>
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    ///<summary>
    /// Save all changes
    ///</summary>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    ///<summary>
    /// Begin transaction
    ///</summary>
    public async Task BeginTransactionAsync()
    {
        _transaction ??= await _context.Database.BeginTransactionAsync();
    }

    ///<summary>
    /// Commit transaction
    ///</summary>
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

    ///<summary>
    /// Rollback transaction
    ///</summary>
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

    ///<summary>
    /// Dispose
    ///</summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ///<summary>
    /// Dispose
    ///</summary>
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
