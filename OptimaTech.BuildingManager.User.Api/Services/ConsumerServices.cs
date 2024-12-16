using FluentValidation;
using OptimaTech.BuildingManager.User.Application;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Application.UseCases.Consumers;
using OptimaTech.BuildingManager.User.Core.Entities;
using OptimaTech.BuildingManager.User.Core.Validators;
using OptimaTech.BuildingManager.User.Infrastructure.Repositories;
using OptimaTech.BuildingManager.User.Infrastructure.UnitOfWork;

namespace OptimaTech.BuildingManager.User.Api.Services;

public static class ConsumerServices
{
    public static void Add(WebApplicationBuilder builder)
    {
        // Add Repository
        builder.Services.AddScoped<IBusinessRepository<ConsumerApplicationModel>, ConsumerRepository>();

        // Add Unit Of Work
        builder.Services.AddScoped<IUnitOfWork<ConsumerApplicationModel>, ConsumerUnitOfWork>();

        // Add Use Cases
        builder.Services.AddScoped<IUseCase<Guid, ConsumerApplicationModel>, FindConsumerUseCase>();
        builder.Services.AddScoped<IUseCase<ConsumerSelectParameter, SelectResult<ConsumerApplicationModel>>, SearchConsumerUseCase>();
        builder.Services.AddKeyedScoped<IUseCase<DeleteParameter, bool>, DeleteConsumerUseCase>("Consumer");
        builder.Services.AddKeyedScoped<IUseCase<ConsumerApplicationModel, bool>, CreateConsumerUseCase>(AppConst.SERVICE_KEY_CREATE);
        builder.Services.AddKeyedScoped<IUseCase<ConsumerApplicationModel, bool>, UpdateConsumerUseCase>(AppConst.SERVICE_KEY_UPDATE);

        // Add Validator
        builder.Services.AddScoped<IValidator<Consumer>, ConsumerValidator>();
    }
}