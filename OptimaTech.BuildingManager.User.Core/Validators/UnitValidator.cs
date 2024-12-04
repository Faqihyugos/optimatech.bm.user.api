using FluentValidation;
using OptimaTech.BuildingManager.User.Core.Entities;
namespace OptimaTech.BuildingManager.User.Core.Validators;

public class UnitValidator : AbstractValidator<Unit>
{
  public UnitValidator()
    {
        RuleFor(t => t.Id).NotEqual(Guid.Empty);
        RuleFor(t => t.TenantId).NotEqual(Guid.Empty);
        RuleFor(t => t.Code).NotNull().NotEmpty();
        RuleFor(t => t.Name).NotNull().NotEmpty();
        RuleFor(t => t.ProjectId).NotEqual(Guid.Empty);
        RuleFor(t => t.BuildingName).NotNull().NotEmpty();
        RuleFor(t => t.UnitNumber).NotNull().NotEmpty();
        RuleFor(t => t.FloorNumber).NotNull().NotEmpty();
        RuleFor(t => t.UnitType).NotNull().NotEmpty();
        RuleFor(t => t.UnitStatus).NotNull().NotEmpty();
    }
}