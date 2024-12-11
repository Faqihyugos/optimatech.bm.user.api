using FluentValidation;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using CoreEntities = OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Users;

public class CreateUserUseCase(IUnitOfWork<UserApplicationModel> uow, IValidator<CoreEntities.User> validator) : IUseCase<UserApplicationModel, bool>
{
    protected readonly IUnitOfWork<UserApplicationModel> _uow = uow;
    protected readonly IValidator<CoreEntities.User> _validator = validator;
    public async Task<bool> Execute(UserApplicationModel input)
    {
        _validator.ValidateAndThrow(input.ToUser());

        await _uow.Repository.InsertAsync(input);
        await _uow.CommitAsync();

        return true;
    }
}