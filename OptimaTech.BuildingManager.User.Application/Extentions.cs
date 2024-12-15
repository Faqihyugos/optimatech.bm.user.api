using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Core.Entities;
using CoreEntities = OptimaTech.BuildingManager.User.Core.Entities;

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

    public static CoreEntities.User ToUser(this UserApplicationModel model)
    {
        return new CoreEntities.User() { Id = model.Id, TenantId = model.TenantId, Code = model.Code, Name = model.Name, UserName = model.UserName, Email = model.Email, Password = model.Password, RoleId = model.RoleId, Occupation = model.Occupation, UserStatus = model.UserStatus, UserType = model.UserType, ApprovalStatus = model.ApprovalStatus, BirthDate = model.BirthDate };
    }

    public static UserUnit ToUserUnit(this UserUnitApplicationModel model)
    {
        return new UserUnit() { Id = model.Id, TenantId = model.TenantId, Code = model.Code, Name = model.Name, UserId = model.UserId, UnitId = model.UnitId, StartDate = model.StartDate, EndDate = model.EndDate, RelationType = model.RelationType, RelationStatus = model.RelationStatus, NoBAST = model.NoBAST, DateBAST = model.DateBAST };
    }
}