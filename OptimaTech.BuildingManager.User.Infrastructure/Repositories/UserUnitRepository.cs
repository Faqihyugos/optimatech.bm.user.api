using OptimaTech.BuildingManager.User.Application.Models;

namespace OptimaTech.BuildingManager.User.Infrastructure.Repositories;

public class UserUnitRepository : BusinessRepositoryBase<UserUnitApplicationModel>
{
    public UserUnitRepository(AppDbContext context) : base(context)
    {

    }
}