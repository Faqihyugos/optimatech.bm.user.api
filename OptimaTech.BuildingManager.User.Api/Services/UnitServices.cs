using FluentValidation;
using OptimaTech.BuildingManager.User.Application;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Application.UseCases.Units;
using OptimaTech.BuildingManager.User.Core.Entities;
using OptimaTech.BuildingManager.User.Core.Validators;
using OptimaTech.BuildingManager.User.Infrastructure.Repositories;
using OptimaTech.BuildingManager.User.Infrastructure.UnitOfWork;

namespace OptimaTech.BuildingManager.User.Api.Services;

public static class UnitServices
{
    public static void Add(WebApplicationBuilder builder)
    {
        // Add Repository
        builder.Services.AddScoped<IBusinessRepository<UnitApplicationModel>, UnitRepository>();

        // Add Unit Of Work
        builder.Services.AddScoped<IUnitOfWork<UnitApplicationModel>, UnitUnitOfWork>();

        // Add Use Cases
        builder.Services.AddScoped<IUseCase<Guid, UnitApplicationModel>, FindUnitUseCase>();
        builder.Services.AddScoped<IUseCase<UnitSelectParameter, SelectResult<UnitApplicationModel>>, SearchUnitUseCase>();
        builder.Services.AddKeyedScoped<IUseCase<DeleteParameter, bool>, DeleteUnitUseCase>("Unit");
        builder.Services.AddKeyedScoped<IUseCase<UnitApplicationModel, bool>, CreateUnitUseCase>(AppConst.SERVICE_KEY_CREATE);
        builder.Services.AddKeyedScoped<IUseCase<UnitApplicationModel, bool>, UpdateUnitUseCase>(AppConst.SERVICE_KEY_UPDATE);

        // Add Validator
        builder.Services.AddScoped<IValidator<Unit>, UnitValidator>();
    }
}