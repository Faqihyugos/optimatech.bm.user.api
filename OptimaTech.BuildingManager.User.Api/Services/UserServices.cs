using FluentValidation;
using OptimaTech.BuildingManager.User.Application;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Application.UseCases.Users;
using CoreEntities = OptimaTech.BuildingManager.User.Core.Entities;
using OptimaTech.BuildingManager.User.Core.Validators;
using OptimaTech.BuildingManager.User.Infrastructure.Repositories;
using OptimaTech.BuildingManager.User.Infrastructure.UserUnitOfWork;

namespace OptimaTech.BuildingManager.User.Api.Services;

public static class UserServices
{
    public static void Add(WebApplicationBuilder builder)
    {
        // Add Repository
        builder.Services.AddScoped<IBusinessRepository<UserApplicationModel>, UserRepository>();

        // Add Unit Of Work
        builder.Services.AddScoped<IUnitOfWork<UserApplicationModel>, UserUnitOfWork>();

        // Add Use Cases
        builder.Services.AddScoped<IUseCase<Guid, UserApplicationModel>, FindUserUseCase>();
        builder.Services.AddScoped<IUseCase<UserSelectParameter, SelectResult<UserApplicationModel>>, SearchUserUseCase>();
        builder.Services.AddKeyedScoped<IUseCase<DeleteParameter, bool>, DeleteUserUseCase>("User");
        builder.Services.AddKeyedScoped<IUseCase<UserApplicationModel, bool>, CreateUserUseCase>(AppConst.SERVICE_KEY_CREATE);
        builder.Services.AddKeyedScoped<IUseCase<UserApplicationModel, bool>, UpdateUserUseCase>(AppConst.SERVICE_KEY_UPDATE);

        // Add Validator
        builder.Services.AddScoped<IValidator<CoreEntities.User>, UserValidator>();
    }
}