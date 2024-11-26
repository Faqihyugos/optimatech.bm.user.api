using FluentValidation;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Tenants;

public class UpdateTenantUseCase(IUnitOfWork<TenantApplicationModel> uow, IValidator<Tenant> validator) : IUseCase<TenantApplicationModel, bool>
{
    protected readonly IUnitOfWork<TenantApplicationModel> _uow = uow;
    protected readonly IValidator<Tenant> _validator = validator;
    public async Task<bool> Execute(TenantApplicationModel input)
    {
        TenantApplicationModel? tenant = await _uow.Repository.FindAsync(input.Id);
        if (tenant != null)
        {
            _validator.ValidateAndThrow(input.ToTenant());

            tenant.Code = input.Code;
            tenant.Name = input.Name;
            tenant.Description = input.Description;
            tenant.UpdatedDate = DateTime.Now;
            tenant.UpdatedUserId = input.UpdatedUserId;

            await _uow.Repository.UpdateAsync(tenant);
            await _uow.CommitAsync();
        }
        else
        {
            throw new Exception(Messages.DATA_NOT_FOUND);
        }

        return true;
    }
}