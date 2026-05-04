using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.DatabaseContext;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterviewTraining.Infrastructure.Repositories;

/// <summary>
/// Репозиторий для работы с дополнительной информацией пользователей
/// </summary>
public class AdditionalUserInfoRepository : Repository<AdditionalUserInfo, Guid>, IAdditionalUserInfoRepository
{
    public AdditionalUserInfoRepository(InterviewContext context) : base(context)
    {
    }

    public async Task<AdditionalUserInfo> GetByIdentityUserIdAsync(string identityUserId, CancellationToken cancellationToken)
    {
        return await DbSet
            .Include(x => x.TimeZone)
            .Include(x => x.Currency)
            .FirstOrDefaultAsync(u => u.IdentityUserId == identityUserId && !u.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<AdditionalUserInfo>> GetExpertsAsync(CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(u => u.IsExpert && !u.IsDeleted && u.IsExpertAvailableInSearch)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<AdditionalUserInfo>> GetCandidatesAsync()
    {
        return await DbSet
            .Where(u => u.IsCandidate && !u.IsDeleted)
            .ToListAsync();
    }

    public async Task<AdditionalUserInfo> GetWithRatingsAsync(Guid id)
    {
        return await DbSet
            .Include(u => u.RatingFromUsers)
            .Include(u => u.MyRatingToUsers)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> ExistsByIdentityUserIdAsync(string identityUserId)
    {
        return await DbSet
            .AnyAsync(u => u.IdentityUserId == identityUserId && !u.IsDeleted);
    }

    public async Task<IEnumerable<AdditionalUserInfo>> SearchExpertsAsync(string searchTerm)
    {
        var term = searchTerm.ToLower();
        return await DbSet
            .Where(u => u.IsExpert && 
                       !u.IsDeleted &&
                       (u.ShortDescription != null && u.ShortDescription.ToLower().Contains(term) ||
                        u.Description != null && u.Description.ToLower().Contains(term)))
            .ToListAsync();
    }

    public override async Task<AdditionalUserInfo> GetByIdAsync(Guid id)
    {
        return await DbSet
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
    }

    public override async Task<IEnumerable<AdditionalUserInfo>> GetAllAsync()
    {
        return await DbSet
            .Where(u => !u.IsDeleted)
            .ToListAsync();
    }
}
