using FluentValidation;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Tenants;

public class CreateTenantUseCase(IUnitOfWork<TenantApplicationModel> uow, IValidator<Tenant> validator) : IUseCase<TenantApplicationModel, bool>
{
    protected readonly IUnitOfWork<TenantApplicationModel> _uow = uow;
    protected readonly IValidator<Tenant> _validator = validator;
    public async Task<bool> Execute(TenantApplicationModel input)
    {
        _validator.ValidateAndThrow(input.ToTenant());

        await _uow.Repository.InsertAsync(input);
        await _uow.CommitAsync();

        return true;
    }
}