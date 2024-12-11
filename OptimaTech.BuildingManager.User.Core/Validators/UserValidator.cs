using FluentValidation;
using CoreEntities = OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Core.Validators;

public class UserValidator : AbstractValidator<CoreEntities.User>
{
    public UserValidator()
    {
        RuleFor(t => t.Id).NotEqual(Guid.Empty);
        RuleFor(t => t.TenantId).NotEqual(Guid.Empty);
        RuleFor(t => t.Code).NotNull().NotEmpty();
        RuleFor(t => t.Name).NotNull().NotEmpty();
        RuleFor(t => t.RoleId).NotEqual(Guid.Empty);
        RuleFor(t => t.UserName).NotNull().NotEmpty();
        RuleFor(t => t.Email).NotNull().NotEmpty();
        RuleFor(t => t.Password).NotNull().NotEmpty();
        RuleFor(t => t.BirthDate).NotNull().NotEmpty();
        RuleFor(t => t.Occupation).NotNull().NotEmpty();
        RuleFor(t => t.UserStatus).NotNull().NotEmpty();
        RuleFor(t => t.UserType).NotNull().NotEmpty();
        RuleFor(t => t.ApprovalStatus).NotNull().NotEmpty();
    }
}