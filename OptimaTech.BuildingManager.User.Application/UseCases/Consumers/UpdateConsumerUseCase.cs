using FluentValidation;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Consumers;

public class UpdateConsumerUseCase(IUnitOfWork<ConsumerApplicationModel> uow, IValidator<Consumer> validator) : IUseCase<ConsumerApplicationModel, bool>
{
    protected readonly IUnitOfWork<ConsumerApplicationModel> _uow = uow;
    protected readonly IValidator<Consumer> _validator = validator;
    public async Task<bool> Execute(ConsumerApplicationModel input)
    {
        ConsumerApplicationModel? Consumer = await _uow.Repository.FindAsync(input.Id);
        if (Consumer != null)
        {
            _validator.ValidateAndThrow(input.ToConsumer());

            Consumer.Code = input.Code;
            Consumer.Name = input.Name;
            Consumer.UserId = input.UserId;
            Consumer.UpdatedDate = DateTime.Now;
            Consumer.UpdatedUserId = input.UpdatedUserId;

            await _uow.Repository.UpdateAsync(Consumer);
            await _uow.CommitAsync();
        }
        else
        {
            throw new Exception(Messages.DATA_NOT_FOUND);
        }

        return true;
    }
}