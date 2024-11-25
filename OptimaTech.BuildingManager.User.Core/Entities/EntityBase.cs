namespace OptimaTech.BuildingManager.User.Core.Entities;
public abstract class EntityBase : IEntity
{
    public required Guid Id { get; set; }
    public required Guid TenantId { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
}