using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Infrastructure.Repositories;

public abstract class BusinessBasicRepositoryBase<T> : BasicRepositoryBase<T>, IBusinessBasicRepository<T> where T : class, IBasic
{
    protected BusinessBasicRepositoryBase(AppDbContext context) : base(context)
    {

    }

    public virtual Task DeleteAsync(Guid parentId)
    {
        throw new NotImplementedException();
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