using FluentValidation;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Roles;

public class UpdateRoleUseCase(IUnitOfWork<RoleApplicationModel> uow, IValidator<Role> validator) : IUseCase<RoleApplicationModel, bool>
{
    protected readonly IUnitOfWork<RoleApplicationModel> _uow = uow;
    protected readonly IValidator<Role> _validator = validator;
    public async Task<bool> Execute(RoleApplicationModel input)
    {
        RoleApplicationModel? Role = await _uow.Repository.FindAsync(input.Id);
        if (Role != null)
        {
            _validator.ValidateAndThrow(input.ToRole());

            Role.Code = input.Code;
            Role.Name = input.Name;
            Role.UpdatedDate = DateTime.Now;
            Role.UpdatedUserId = input.UpdatedUserId;

            await _uow.Repository.UpdateAsync(Role);
            await _uow.CommitAsync();
        }
        else
        {
            throw new Exception(Messages.DATA_NOT_FOUND);
        }

        return true;
    }
}