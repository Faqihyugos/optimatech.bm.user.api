using OptimaTech.BuildingManager.User.Application.Models;

namespace OptimaTech.BuildingManager.User.Infrastructure.Repositories;

public class TenantRepository : BusinessRepositoryBase<TenantApplicationModel>
{
    public TenantRepository(AppDbContext context) : base(context)
    {

    }
}