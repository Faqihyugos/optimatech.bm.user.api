using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Infrastructure.Repositories;

namespace OptimaTech.BuildingManager.User.Infrastructure.UnitOfWork;

public class RoleUnitOfWork(AppDbContext context, IBusinessRepository<RoleApplicationModel> repository) : IUnitOfWork<RoleApplicationModel>
{
    protected readonly AppDbContext _context = context;
    protected readonly IBusinessRepository<RoleApplicationModel> _repository = repository;
    public IBusinessRepository<RoleApplicationModel> Repository { get => _repository; }

    public Task CommitAsync()
    {
        return _context.SaveChangesAsync();
    }
}