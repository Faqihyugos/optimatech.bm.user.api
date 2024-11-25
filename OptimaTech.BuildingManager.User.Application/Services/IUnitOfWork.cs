namespace OptimaTech.BuildingManager.User.Application.Services;

public interface IUnitOfWork
{
    Task CommitAsync();
}