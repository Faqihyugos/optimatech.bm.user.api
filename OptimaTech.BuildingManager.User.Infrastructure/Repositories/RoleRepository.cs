using OptimaTech.BuildingManager.User.Application.Models;

namespace OptimaTech.BuildingManager.User.Infrastructure.Repositories;

public class RoleRepository : BusinessRepositoryBase<RoleApplicationModel>
{
    public RoleRepository(AppDbContext context) : base(context)
    {

    }
}