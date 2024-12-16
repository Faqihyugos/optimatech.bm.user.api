using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Consumers;

 public class FindConsumerUseCase(IBusinessRepository<ConsumerApplicationModel> repository) : IUseCase<Guid, ConsumerApplicationModel>
    {
        protected readonly IBusinessRepository<ConsumerApplicationModel> _repository = repository;
        public async Task<ConsumerApplicationModel?> Execute(Guid input)
        {
            return await _repository.FindAsync(input);
        }
    }