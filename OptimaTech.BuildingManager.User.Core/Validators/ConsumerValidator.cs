using FluentValidation;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Core.Validators;

public class ConsumerValidator : AbstractValidator<Consumer>
{
    public ConsumerValidator()
    {
        RuleFor(t => t.Id).NotEqual(Guid.Empty);
        RuleFor(t => t.TenantId).NotEqual(Guid.Empty);
        RuleFor(t => t.UserId).NotEqual(Guid.Empty);
        RuleFor(t => t.Code).NotNull().NotEmpty();
        RuleFor(t => t.Name).NotNull().NotEmpty();
    }
}