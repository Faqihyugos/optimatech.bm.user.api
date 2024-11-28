namespace OptimaTech.BuildingManager.User.Core.Entities;

public class Consumer : EntityBase
{
  public required Guid UserId { get; set; }

  public User? User { get; set; }
}