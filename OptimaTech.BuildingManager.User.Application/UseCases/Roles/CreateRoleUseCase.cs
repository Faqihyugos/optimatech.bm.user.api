using FluentValidation;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Roles;

public class CreateRoleUseCase(IUnitOfWork<RoleApplicationModel> uow, IValidator<Role> validator) : IUseCase<RoleApplicationModel, bool>
{
    protected readonly IUnitOfWork<RoleApplicationModel> _uow = uow;
    protected readonly IValidator<Role> _validator = validator;
    public async Task<bool> Execute(RoleApplicationModel input)
    {
        _validator.ValidateAndThrow(input.ToRole());

        await _uow.Repository.InsertAsync(input);
        await _uow.CommitAsync();

        return true;
    }
}