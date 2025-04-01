using Data.Contexts;
using Data.Entitites;
using Domain.Responses;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface IUserDismissedNotificationRepository : IBaseRepository<UserDismissedNotificationEntity>
{
    Task<RepositoryResult<IEnumerable<string>>> GetNoticationsIdsAsync(string userId);
}

public class UserDismissedNotificationRepository(AppDbContext context) : BaseRepository<UserDismissedNotificationEntity>(context), IUserDismissedNotificationRepository
{
    public async Task<RepositoryResult<IEnumerable<string>>> GetNoticationsIdsAsync(string userId)
    {
        var ids = await _table.Where(x => x.UserId == userId).Select(x => x.NotificationId).ToListAsync();
        return new RepositoryResult<IEnumerable<string>> { Succeeded = true , StatusCode = 200, Result = ids};
    }
}