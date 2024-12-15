using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Infrastructure.Repositories;

namespace OptimaTech.BuildingManager.User.Infrastructure.UnitOfWork;

public class UserUnitUnitOfWork(AppDbContext context, IBusinessRepository<UserUnitApplicationModel> repository) : IUnitOfWork<UserUnitApplicationModel>
{
    protected readonly AppDbContext _context = context;
    protected readonly IBusinessRepository<UserUnitApplicationModel> _repository = repository;
    public IBusinessRepository<UserUnitApplicationModel> Repository { get => _repository; }

    public Task CommitAsync()
    {
        return _context.SaveChangesAsync();
    }
}