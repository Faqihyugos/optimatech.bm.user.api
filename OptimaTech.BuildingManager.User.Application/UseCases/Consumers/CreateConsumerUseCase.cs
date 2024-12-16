using FluentValidation;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Consumers;

public class CreateConsumerUseCase(IUnitOfWork<ConsumerApplicationModel> uow, IValidator<Consumer> validator) : IUseCase<ConsumerApplicationModel, bool>
{
    protected readonly IUnitOfWork<ConsumerApplicationModel> _uow = uow;
    protected readonly IValidator<Consumer> _validator = validator;
    public async Task<bool> Execute(ConsumerApplicationModel input)
    {
        _validator.ValidateAndThrow(input.ToConsumer());

        await _uow.Repository.InsertAsync(input);
        await _uow.CommitAsync();

        return true;
    }
}