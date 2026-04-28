using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Repositories.Interfaces;

///<summary>
/// Unit of Work interface
///</summary>
public interface IUnitOfWork : IDisposable
{
    ///<summary>
    /// Skills repository
    ///</summary>
    ISkillRepository Skills { get; }

    ///<summary>
    /// Skill groups repository
    ///</summary>
    ISkillGroupRepository SkillGroups { get; }

    ///<summary>
    /// Skill tags repository
    ///</summary>
    ISkillTagRepository SkillTags { get; }

    ///<summary>
    /// User ratings repository
    ///</summary>
    IUserRatingRepository UserRatings { get; }

    ///<summary>
    /// Additional user info repository
    ///</summary>
    IAdditionalUserInfoRepository AdditionalUserInfos { get; }

    ///<summary>
    /// Time zones repository
    ///</summary>
    ITimeZoneRepository TimeZones { get; }

    ///<summary>
    /// User skills repository
    ///</summary>
    IUserSkillRepository UserSkills { get; }

    ///<summary>
    /// User available times repository
    ///</summary>
    IUserAvailableTimeRepository UserAvailableTimes { get; }

    ///<summary>
    /// Interviews repository
    ///</summary>
    IInterviewRepository Interviews { get; }

    ///<summary>
    /// Interview versions repository
    ///</summary>
    IInterviewVersionRepository InterviewVersions { get; }

    ///<summary>
    /// Interview languages repository
    ///</summary>
    IInterviewLanguageRepository InterviewLanguages { get; }

    ///<summary>
    /// Currencies repository
    ///</summary>
    ICurrencyRepository Currencies { get; }

    ///<summary>
    /// Interview chat messages repository
    ///</summary>
    IInterviewChatMessageRepository InterviewChatMessages { get; }

    ///<summary>
    /// User notifications repository
    ///</summary>
    IUserNotificationRepository UserNotifications { get; }

    ///<summary>
    /// User chat messages repository
    ///</summary>
    IUserChatMessageRepository UserChatMessages { get; }

    ///<summary>
    /// Save all changes
    ///</summary>
    Task<int> SaveChangesAsync();

    ///<summary>
    /// Save all changes
    ///</summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    ///<summary>
    /// Begin transaction
    ///</summary>
    Task BeginTransactionAsync();

    ///<summary>
    /// Commit transaction
    ///</summary>
    Task CommitTransactionAsync();

    ///<summary>
    /// Rollback transaction
    ///</summary>
    Task RollbackTransactionAsync();
}
