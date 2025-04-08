using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public interface IProjectRepository : IBaseRepository<ProjectEntity>
{
}

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    public override async Task<IEnumerable<ProjectEntity>> GetAllAsync()
    {
        var entities = await _table
            .Include(x => x.User)
            .Include(x => x.Status)
            .Include(x => x.Client)
            .ToListAsync();

        return entities;
    }

    public override async Task<ProjectEntity?> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var entity = await _table
            .Include(x => x.User)
            .Include(x => x.Status)
            .Include(x => x.Client)
            .FirstOrDefaultAsync(expression);

        return entity;
    }
}
