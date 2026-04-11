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
/// Репозиторий для работы с рейтингами пользователей
/// </summary>
public class UserRatingRepository : Repository<UserRating, Guid>, IUserRatingRepository
{
    public UserRatingRepository(InterviewContext context) : base(context)
    {
    }

    public async Task<IEnumerable<UserRating>> GetByUserFromIdAsync(Guid userFromId)
    {
        return await DbSet
            .Include(r => r.UserTo)
            .Where(r => r.UserFromId == userFromId && !r.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserRating>> GetByUserToIdAsync(Guid userToId)
    {
        return await DbSet
            .Include(r => r.UserFrom)
            .Where(r => r.UserToId == userToId && !r.IsDeleted)
            .ToListAsync();
    }

    public async Task<UserRating> GetWithUsersAsync(Guid id)
    {
        return await DbSet
            .Include(r => r.UserFrom)
            .Include(r => r.UserTo)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<bool> ExistsRatingAsync(Guid userFromId, Guid userToId)
    {
        return await DbSet
            .AnyAsync(r => r.UserFromId == userFromId && 
                          r.UserToId == userToId && 
                          !r.IsDeleted);
    }

    public async Task<double> GetAverageRatingAsync(Guid userToId)
    {
        // Примечание: для расчета среднего рейтинга нужно добавить поле RatingValue в сущность UserRating
        // Сейчас возвращаем заглушку
        var ratings = await DbSet
            .Where(r => r.UserToId == userToId && !r.IsDeleted)
            .CountAsync();

        return ratings > 0 ? 1.0 : 0.0;
    }

    public async Task<int> GetRatingCountAsync(Guid userToId)
    {
        return await DbSet
            .CountAsync(r => r.UserToId == userToId && !r.IsDeleted);
    }

    public override async Task<UserRating> GetByIdAsync(Guid id)
    {
        return await DbSet
            .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
    }

    public override async Task<IEnumerable<UserRating>> GetAllAsync()
    {
        return await DbSet
            .Where(r => !r.IsDeleted)
            .ToListAsync();
    }
}
