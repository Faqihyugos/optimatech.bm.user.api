using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.Services;

public interface IBusinessBasicRepository<T> : IRepository<T> where T : class, IBasic
{
    Task InsertAsync(T model);
    Task UpdateAsync(T model);
    Task DeleteAsync(Guid parentId);
}