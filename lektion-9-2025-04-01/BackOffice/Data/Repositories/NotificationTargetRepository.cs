using Data.Contexts;
using Data.Entitites;

namespace Data.Repositories;

public interface INotificationTargetRepository : IBaseRepository<NotificationTargetEntity>
{

}


public class NotificationTargetRepository(AppDbContext context) : BaseRepository<NotificationTargetEntity>(context), INotificationTargetRepository
{

}
