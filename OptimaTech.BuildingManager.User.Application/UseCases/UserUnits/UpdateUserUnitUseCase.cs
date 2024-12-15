using FluentValidation;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.UseCases.UserUnits;

public class UpdateUserUnitUseCase(IUnitOfWork<UserUnitApplicationModel> uow, IValidator<UserUnit> validator) : IUseCase<UserUnitApplicationModel, bool>
{
    protected readonly IUnitOfWork<UserUnitApplicationModel> _uow = uow;
    protected readonly IValidator<UserUnit> _validator = validator;
    public async Task<bool> Execute(UserUnitApplicationModel input)
    {
        UserUnitApplicationModel? UserUnit = await _uow.Repository.FindAsync(input.Id);
        if (UserUnit != null)
        {
            _validator.ValidateAndThrow(input.ToUserUnit());

            UserUnit.Code = input.Code;
            UserUnit.Name = input.Name;
            UserUnit.UserId = input.UserId;
            UserUnit.UnitId = input.UnitId;
            UserUnit.StartDate = input.StartDate;
            UserUnit.EndDate = input.EndDate;
            UserUnit.RelationType = input.RelationType;
            UserUnit.RelationStatus = input.RelationStatus;
            UserUnit.NoBAST = input.NoBAST;
            UserUnit.DateBAST = input.DateBAST;

            UserUnit.UpdatedDate = DateTime.Now;
            UserUnit.UpdatedUserId = input.UpdatedUserId;

            await _uow.Repository.UpdateAsync(UserUnit);
            await _uow.CommitAsync();
        }
        else
        {
            throw new Exception(Messages.DATA_NOT_FOUND);
        }

        return true;
    }
}