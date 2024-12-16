using OptimaTech.BuildingManager.User.Application.Models;

namespace OptimaTech.BuildingManager.User.Infrastructure.Repositories;

public class ConsumerRepository : BusinessRepositoryBase<ConsumerApplicationModel>
{
    public ConsumerRepository(AppDbContext context) : base(context)
    {

    }
}