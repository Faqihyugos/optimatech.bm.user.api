using OptimaTech.BuildingManager.User.Core.Entities;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
namespace OptimaTech.BuildingManager.User.Application.Services;

public interface IRepository<T> where T : class, IBasic
{
    Task<bool> ExistAsync(Guid id);
    Task<T?> FindAsync(Guid id);
    Task<SelectResult<T>> SelectAsync(ISelectParameter parameter);
}