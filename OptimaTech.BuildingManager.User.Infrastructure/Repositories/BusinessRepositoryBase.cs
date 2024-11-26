using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Infrastructure.Repositories;

public abstract class BusinessRepositoryBase<T> : RepositoryBase<T>, IBusinessRepository<T> where T : class, IApplicationModel
{
    protected BusinessRepositoryBase(AppDbContext context) : base(context)
    {

    }

    public virtual Task DeleteAsync(Guid id, Guid userId)
    {
        return Task.Run(() =>
        {
            T? entity = FindAsync(id).Result;

            if (entity != null)
            {
                entity.Deleted = true;
                entity.DeletedDate = DateTime.Now;
                entity.DeletedUserId = userId;
            }
        });
    }

    public virtual Task InsertAsync(T entity)
    {
        return Task.Run(() => { _context.Set<T>().Add(entity); });
    }

    public virtual Task UpdateAsync(T entity)
    {
        return Task.Run(() => { _context.Set<T>().Update(entity); });
    }
}