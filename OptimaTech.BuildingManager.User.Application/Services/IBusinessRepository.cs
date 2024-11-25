using OptimaTech.BuildingManager.User.Application.Models;

namespace OptimaTech.BuildingManager.User.Application.Services;

 public interface IBusinessRepository<T> : IRepository<T> where T : class, IApplicationModel
    {
        Task InsertAsync(T model);
        Task UpdateAsync(T model);
        Task DeleteAsync(Guid id, Guid userId);
    }