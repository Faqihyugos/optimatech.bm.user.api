using FluentValidation;
using CoreEntities = OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Core.Validators;

public class UserUnitValidator : AbstractValidator<CoreEntities.UserUnit>
{
    public UserUnitValidator()
    {
        RuleFor(t => t.Id).NotEqual(Guid.Empty);
        RuleFor(t => t.TenantId).NotEqual(Guid.Empty);
        RuleFor(t => t.UserId).NotEqual(Guid.Empty);
        RuleFor(t => t.UnitId).NotEqual(Guid.Empty);
        RuleFor(t => t.StartDate).NotNull().NotEmpty();
        RuleFor(t => t.EndDate).NotNull().NotEmpty();
        RuleFor(t => t.RelationType).NotNull().NotEmpty();
        RuleFor(t => t.RelationStatus).NotNull().NotEmpty();
        RuleFor(t => t.NoBAST).NotNull().NotEmpty();
        RuleFor(t => t.DateBAST).NotNull().NotEmpty();
    }
}