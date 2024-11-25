namespace OptimaTech.BuildingManager.User.Core.Entities;

public interface IBasic
{
    Guid Id { get; set; }
    Guid TenantId { get; set; }
}