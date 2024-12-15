using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Infrastructure.Repositories;

namespace OptimaTech.BuildingManager.User.Infrastructure.UnitOfWork;

public class UserUnitOfWork(AppDbContext context, IBusinessRepository<UserApplicationModel> repository) : IUnitOfWork<UserApplicationModel>
{
    protected readonly AppDbContext _context = context;
    protected readonly IBusinessRepository<UserApplicationModel> _repository = repository;
    public IBusinessRepository<UserApplicationModel> Repository { get => _repository; }

    public Task CommitAsync()
    {
        return _context.SaveChangesAsync();
    }
}