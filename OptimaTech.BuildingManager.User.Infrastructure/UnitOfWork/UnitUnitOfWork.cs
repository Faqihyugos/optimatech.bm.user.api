using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Infrastructure.Repositories;

namespace OptimaTech.BuildingManager.User.Infrastructure.UnitOfWork;

public class UnitUnitOfWork(AppDbContext context, IBusinessRepository<UnitApplicationModel> repository) : IUnitOfWork<UnitApplicationModel>
{
    protected readonly AppDbContext _context = context;
    protected readonly IBusinessRepository<UnitApplicationModel> _repository = repository;
    public IBusinessRepository<UnitApplicationModel> Repository { get => _repository; }

    public Task CommitAsync()
    {
        return _context.SaveChangesAsync();
    }
}