using OptimaTech.BuildingManager.User.Application.Models;

namespace OptimaTech.BuildingManager.User.Infrastructure.Repositories;

public class ProjectRepository : BusinessRepositoryBase<ProjectApplicationModel>
{
    public ProjectRepository(AppDbContext context) : base(context)
    {
    }
}