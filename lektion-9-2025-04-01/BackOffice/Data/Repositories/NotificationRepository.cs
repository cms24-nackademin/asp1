using Data.Contexts;
using Data.Entitites;

namespace Data.Repositories;

public interface INotificationRepository : IBaseRepository<NotificationEntity>
{

}


public class NotificationRepository(AppDbContext context) : BaseRepository<NotificationEntity>(context), INotificationRepository
{

}
