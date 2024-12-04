using FluentValidation;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Units;

public class CreateUnitUseCase(IUnitOfWork<UnitApplicationModel> uow, IValidator<Unit> validator) : IUseCase<UnitApplicationModel, bool>
{
    protected readonly IUnitOfWork<UnitApplicationModel> _uow = uow;
    protected readonly IValidator<Unit> _validator = validator;
    public async Task<bool> Execute(UnitApplicationModel input)
    {
        _validator.ValidateAndThrow(input.ToUnit());

        await _uow.Repository.InsertAsync(input);
        await _uow.CommitAsync();

        return true;
    }
}