using OptimaTech.BuildingManager.User.Application.Models;

namespace OptimaTech.BuildingManager.User.Infrastructure.Repositories;

public class UserRepository : BusinessRepositoryBase<UserApplicationModel>
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
}