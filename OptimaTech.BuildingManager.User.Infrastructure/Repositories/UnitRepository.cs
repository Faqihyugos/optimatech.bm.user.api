using OptimaTech.BuildingManager.User.Application.Models;

namespace OptimaTech.BuildingManager.User.Infrastructure.Repositories;

public class UnitRepository : BusinessRepositoryBase<UnitApplicationModel>
{
    public UnitRepository(AppDbContext context) : base(context)
    {
    }
}