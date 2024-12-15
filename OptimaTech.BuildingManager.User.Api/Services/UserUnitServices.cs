using FluentValidation;
using OptimaTech.BuildingManager.User.Application;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Application.UseCases.UserUnits;
using OptimaTech.BuildingManager.User.Core.Entities;
using OptimaTech.BuildingManager.User.Core.Validators;
using OptimaTech.BuildingManager.User.Infrastructure.Repositories;
using OptimaTech.BuildingManager.User.Infrastructure.UnitOfWork;

namespace OptimaTech.BuildingManager.User.Api.Services;

public static class UserUnitServices
{
    public static void Add(WebApplicationBuilder builder)
    {
        // Add Repository
        builder.Services.AddScoped<IBusinessRepository<UserUnitApplicationModel>, UserUnitRepository>();

        // Add Unit Of Work
        builder.Services.AddScoped<IUnitOfWork<UserUnitApplicationModel>, UserUnitUnitOfWork>();

        // Add Use Cases
        builder.Services.AddScoped<IUseCase<Guid, UserUnitApplicationModel>, FindUserUnitUseCase>();
        builder.Services.AddScoped<IUseCase<UserUnitSelectParameter, SelectResult<UserUnitApplicationModel>>, SearchUserUnitUseCase>();
        builder.Services.AddKeyedScoped<IUseCase<DeleteParameter, bool>, DeleteUserUnitUseCase>("UserUnit");
        builder.Services.AddKeyedScoped<IUseCase<UserUnitApplicationModel, bool>, CreateUserUnitUseCase>(AppConst.SERVICE_KEY_CREATE);
        builder.Services.AddKeyedScoped<IUseCase<UserUnitApplicationModel, bool>, UpdateUserUnitUseCase>(AppConst.SERVICE_KEY_UPDATE);

        // Add Validator
        builder.Services.AddScoped<IValidator<UserUnit>, UserUnitValidator>();
    }
}