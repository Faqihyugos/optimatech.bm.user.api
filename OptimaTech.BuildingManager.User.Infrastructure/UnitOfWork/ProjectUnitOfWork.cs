using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Infrastructure.Repositories;

namespace OptimaTech.BuildingManager.User.Infrastructure.UnitOfWork;

public class ProjectUnitOfWork(AppDbContext context, IBusinessRepository<ProjectApplicationModel> repository) : IUnitOfWork<ProjectApplicationModel>
{
    protected readonly AppDbContext _context = context;
    protected readonly IBusinessRepository<ProjectApplicationModel> _repository = repository;
    public IBusinessRepository<ProjectApplicationModel> Repository { get => _repository; }

    public Task CommitAsync()
    {
        return _context.SaveChangesAsync();
    }
}