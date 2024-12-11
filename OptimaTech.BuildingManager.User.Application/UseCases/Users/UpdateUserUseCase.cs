using FluentValidation;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using CoreEntities = OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Users;

public class UpdateUserUseCase(IUnitOfWork<UserApplicationModel> uow, IValidator<CoreEntities.User> validator) : IUseCase<UserApplicationModel, bool>
{
    protected readonly IUnitOfWork<UserApplicationModel> _uow = uow;
    protected readonly IValidator<CoreEntities.User> _validator = validator;
    public async Task<bool> Execute(UserApplicationModel input)
    {
        UserApplicationModel? User = await _uow.Repository.FindAsync(input.Id);
        if (User != null)
        {
            _validator.ValidateAndThrow(input.ToUser());

            User.Code = input.Code;
            User.Name = input.Name;
            User.Email = input.Email;
            User.UserName = input.UserName;
            User.Password = input.Password;
            User.BirthDate = input.BirthDate;
            User.RoleId = input.RoleId;
            User.NIK = input.NIK;
            User.NPWP = input.NPWP;
            User.Address = input.Address;
            User.Village = input.Village;
            User.District = input.District;
            User.City = input.City;
            User.Province = input.Province;
            User.MobilePhone = input.MobilePhone;
            User.Religion = input.Religion;
            User.Occupation = input.Occupation;
            User.UserStatus = input.UserStatus;
            User.UserType = input.UserType;
            User.ApprovalStatus = input.ApprovalStatus;
            User.Occupation = input.Occupation;
            User.UpdatedDate = DateTime.Now;
            User.UpdatedUserId = input.UpdatedUserId;

            await _uow.Repository.UpdateAsync(User);
            await _uow.CommitAsync();
        }
        else
        {
            throw new Exception(Messages.DATA_NOT_FOUND);
        }

        return true;
    }
}