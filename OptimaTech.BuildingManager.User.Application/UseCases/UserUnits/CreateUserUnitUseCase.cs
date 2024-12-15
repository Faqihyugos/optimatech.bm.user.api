using FluentValidation;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.UseCases.UserUnits;

public class CreateUserUnitUseCase(IUnitOfWork<UserUnitApplicationModel> uow, IValidator<UserUnit> validator) : IUseCase<UserUnitApplicationModel, bool>
{
    protected readonly IUnitOfWork<UserUnitApplicationModel> _uow = uow;
    protected readonly IValidator<UserUnit> _validator = validator;
    public async Task<bool> Execute(UserUnitApplicationModel input)
    {
        _validator.ValidateAndThrow(input.ToUserUnit());

        await _uow.Repository.InsertAsync(input);
        await _uow.CommitAsync();

        return true;
    }
}