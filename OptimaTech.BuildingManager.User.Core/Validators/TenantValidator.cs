using FluentValidation;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Core.Validators;
public class TenantValidator : AbstractValidator<Tenant>
{
    public TenantValidator()
        {
            RuleFor(t => t.Id).NotEqual(Guid.Empty);
            RuleFor(t => t.TenantId).NotEqual(Guid.Empty);           
            RuleFor(t => t.Code).NotNull().NotEmpty();
            RuleFor(t => t.Name).NotNull().NotEmpty();     
        }
}