using FluentValidation;
using OptimaTech.BuildingManager.User.Application;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Application.UseCases.Roles;
using OptimaTech.BuildingManager.User.Core.Entities;
using OptimaTech.BuildingManager.User.Core.Validators;
using OptimaTech.BuildingManager.User.Infrastructure.Repositories;
using OptimaTech.BuildingManager.User.Infrastructure.UnitOfWork;

namespace OptimaTech.BuildingManager.User.Api.Services;

public static class RoleServices
{
    public static void Add(WebApplicationBuilder builder)
    {
        // Add Repository
        builder.Services.AddScoped<IBusinessRepository<RoleApplicationModel>, RoleRepository>();

        // Add Unit Of Work
        builder.Services.AddScoped<IUnitOfWork<RoleApplicationModel>, RoleUnitOfWork>();

        // Add Use Cases
        builder.Services.AddScoped<IUseCase<Guid, RoleApplicationModel>, FindRoleUseCase>();
        builder.Services.AddScoped<IUseCase<RoleSelectParameter, SelectResult<RoleApplicationModel>>, SearchRoleUseCase>();
        builder.Services.AddKeyedScoped<IUseCase<DeleteParameter, bool>, DeleteRoleUseCase>("Role");
        builder.Services.AddKeyedScoped<IUseCase<RoleApplicationModel, bool>, CreateRoleUseCase>(AppConst.SERVICE_KEY_CREATE);
        builder.Services.AddKeyedScoped<IUseCase<RoleApplicationModel, bool>, UpdateRoleUseCase>(AppConst.SERVICE_KEY_UPDATE);

        // Add Validator
        builder.Services.AddScoped<IValidator<Role>, RoleValidator>();
    }
}