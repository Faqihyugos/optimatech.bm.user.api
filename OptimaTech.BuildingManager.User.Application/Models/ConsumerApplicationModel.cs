namespace OptimaTech.BuildingManager.User.Application.Models;

public class ConsumerApplicationModel : ApplicationModelBase
{
    public required Guid UserId { get; set; }

    public UserApplicationModel? User { get; set; }
}