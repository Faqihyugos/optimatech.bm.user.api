using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Infrastructure.Repositories;

namespace OptimaTech.BuildingManager.User.Infrastructure.UnitOfWork;

public class ConsumerUnitOfWork(AppDbContext context, IBusinessRepository<ConsumerApplicationModel> repository) : IUnitOfWork<ConsumerApplicationModel>
{
    protected readonly AppDbContext _context = context;
    protected readonly IBusinessRepository<ConsumerApplicationModel> _repository = repository;
    public IBusinessRepository<ConsumerApplicationModel> Repository { get => _repository; }

    public Task CommitAsync()
    {
        return _context.SaveChangesAsync();
    }
}