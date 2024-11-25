using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application;

public static class Extensions
{
    public static Tenant ToTenant(this TenantApplicationModel model)
    {
        return new Tenant() { Id = model.Id, TenantId = model.TenantId, Code = model.Code, Name = model.Name, Description = model.Description };
    }
}