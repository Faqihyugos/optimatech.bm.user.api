using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Infrastructure.Repositories;

namespace OptimaTech.BuildingManager.User.Infrastructure.UnitOfWork;

public class TenantUnitOfWork(AppDbContext context, IBusinessRepository<TenantApplicationModel> repository) : IUnitOfWork<TenantApplicationModel>
{
    protected readonly AppDbContext _context = context;
    protected readonly IBusinessRepository<TenantApplicationModel> _repository = repository;
    public IBusinessRepository<TenantApplicationModel> Repository { get => _repository; }

    public Task CommitAsync()
    {
        return _context.SaveChangesAsync();
    }
}