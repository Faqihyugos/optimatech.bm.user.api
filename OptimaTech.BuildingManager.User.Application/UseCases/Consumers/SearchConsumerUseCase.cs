using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Consumers;

public class SearchConsumerUseCase(IBusinessRepository<ConsumerApplicationModel> repository) : IUseCase<ConsumerSelectParameter, SelectResult<ConsumerApplicationModel>>
{
    protected readonly IBusinessRepository<ConsumerApplicationModel> _repository = repository;
    public async Task<SelectResult<ConsumerApplicationModel>?> Execute(ConsumerSelectParameter input)
    {
        return await _repository.SelectAsync(input);
    }
}