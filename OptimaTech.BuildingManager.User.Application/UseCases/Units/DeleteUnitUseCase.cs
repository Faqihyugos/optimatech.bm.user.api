using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Units;

public class DeleteUnitUseCase(IUnitOfWork<UnitApplicationModel> uow) : IUseCase<DeleteParameter, bool>
{
    protected readonly IUnitOfWork<UnitApplicationModel> _uow = uow;
    public async Task<bool> Execute(DeleteParameter input)
    {
        await _uow.Repository.DeleteAsync(input.Id, input.UserId);
        await _uow.CommitAsync();

        return true;
    }
}