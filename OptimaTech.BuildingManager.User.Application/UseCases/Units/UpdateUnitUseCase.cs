using FluentValidation;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Units;

public class UpdateUnitUseCase(IUnitOfWork<UnitApplicationModel> uow, IValidator<Unit> validator) : IUseCase<UnitApplicationModel, bool>
{
    protected readonly IUnitOfWork<UnitApplicationModel> _uow = uow;
    protected readonly IValidator<Unit> _validator = validator;
    public async Task<bool> Execute(UnitApplicationModel input)
    {
        UnitApplicationModel? Unit = await _uow.Repository.FindAsync(input.Id);
        if (Unit != null)
        {
            _validator.ValidateAndThrow(input.ToUnit());
            Unit.Code = input.Code;
            Unit.Name = input.Name;
            Unit.ProjectId = input.ProjectId;
            Unit.BuildingName = input.BuildingName;
            Unit.FloorNumber = input.FloorNumber;
            Unit.UnitNumber = input.UnitNumber;
            Unit.UnitStatus = input.UnitStatus;
            Unit.UnitType = input.UnitType;
            Unit.UpdatedDate = DateTime.Now;
            Unit.UpdatedUserId = input.UpdatedUserId;

            await _uow.Repository.UpdateAsync(Unit);
            await _uow.CommitAsync();
        }
        else
        {
            throw new Exception(Messages.DATA_NOT_FOUND);
        }

        return true;
    }
}