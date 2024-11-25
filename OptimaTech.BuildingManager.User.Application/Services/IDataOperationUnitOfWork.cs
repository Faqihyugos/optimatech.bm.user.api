
using OptimaTech.BuildingManager.User.Application.Models;

namespace OptimaTech.BuildingManager.User.Application.Services;

public interface IUnitOfWork<T> : IUnitOfWork where T : class, IApplicationModel
{
    IBusinessRepository<T> Repository { get; }
}