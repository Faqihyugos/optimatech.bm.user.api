using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application;

public static class Extensions
{
    public static Tenant ToTenant(this TenantApplicationModel model)
    {
        return new Tenant() { Id = model.Id, TenantId = model.TenantId, Code = model.Code, Name = model.Name, Description = model.Description };
    }

    public static Role ToRole(this RoleApplicationModel model)
    {
        return new Role() { Id = model.Id, TenantId = model.TenantId, Code = model.Code, Name = model.Name};
    }

    public static Project ToProject(this ProjectApplicationModel model)
    {
        return new Project() { Id = model.Id, TenantId = model.TenantId, Code = model.Code, Name = model.Name};
    }

    public static Unit ToUnit(this UnitApplicationModel model)
    {
        return new Unit() { Id = model.Id, TenantId = model.TenantId, Code = model.Code, Name = model.Name, ProjectId = model.ProjectId, BuildingName = model.BuildingName, FloorNumber = model.FloorNumber, UnitNumber = model.UnitNumber, UnitStatus = model.UnitStatus, UnitType = model.UnitType };
    }
}