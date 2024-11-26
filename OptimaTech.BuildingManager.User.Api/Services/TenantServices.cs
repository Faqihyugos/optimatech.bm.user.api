using FluentValidation;
using OptimaTech.BuildingManager.User.Application;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Application.UseCases.Tenants;
using OptimaTech.BuildingManager.User.Core.Entities;
using OptimaTech.BuildingManager.User.Core.Validators;
using OptimaTech.BuildingManager.User.Infrastructure.Repositories;
using OptimaTech.BuildingManager.User.Infrastructure.UnitOfWork;

namespace OptimaTech.BuildingManager.User.Api.Services;

public static class TenantServices
{
    public static void Add(WebApplicationBuilder builder)
    {
        // Add Repository
        builder.Services.AddScoped<IBusinessRepository<TenantApplicationModel>, TenantRepository>();

        // Add Unit Of Work
        builder.Services.AddScoped<IUnitOfWork<TenantApplicationModel>, TenantUnitOfWork>();

        // Add Use Cases
        builder.Services.AddScoped<IUseCase<Guid, TenantApplicationModel>, FindTenantUseCase>();
        builder.Services.AddScoped<IUseCase<TenantSelectParameter, SelectResult<TenantApplicationModel>>, SearchTenantUseCase>();
        builder.Services.AddKeyedScoped<IUseCase<DeleteParameter, bool>, DeleteTenantUseCase>("Tenant");
        builder.Services.AddKeyedScoped<IUseCase<TenantApplicationModel, bool>, CreateTenantUseCase>(AppConst.SERVICE_KEY_CREATE);
        builder.Services.AddKeyedScoped<IUseCase<TenantApplicationModel, bool>, UpdateTenantUseCase>(AppConst.SERVICE_KEY_UPDATE);

        // Add Validator
        builder.Services.AddScoped<IValidator<Tenant>, TenantValidator>();
    }
}