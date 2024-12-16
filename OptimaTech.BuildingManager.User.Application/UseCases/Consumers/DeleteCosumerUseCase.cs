using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Consumers;

public class DeleteConsumerUseCase(IUnitOfWork<ConsumerApplicationModel> uow) : IUseCase<DeleteParameter, bool>
{
    protected readonly IUnitOfWork<ConsumerApplicationModel> _uow = uow;
    public async Task<bool> Execute(DeleteParameter input)
    {
        await _uow.Repository.DeleteAsync(input.Id, input.UserId);
        await _uow.CommitAsync();

        return true;
    }
}