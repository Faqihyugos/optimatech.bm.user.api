using Microsoft.EntityFrameworkCore;
using OptimaTech.BuildingManager.User.Application;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Infrastructure.Repositories;

public abstract class BasicRepositoryBase<T> : IRepository<T> where T : class, IBasic
{
    protected readonly AppDbContext _context;
    public BasicRepositoryBase(AppDbContext context)
    {
        _context = context;
    }
    public virtual async Task<bool> ExistAsync(Guid id)
    {
        return await _context.Set<T>().AnyAsync(t => t.Id == id);
    }
    public virtual async Task<T?> FindAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual async Task<SelectResult<T>> SelectAsync(ISelectParameter selectParameter)
    {
        // Base query
        var query = _context.Set<T>().AsQueryable();

        query = query.Where(p => p.TenantId == selectParameter.TenantId);

        // Dynamic sorting
        query = selectParameter.SortBy switch
        {
            _ => selectParameter.SortDirection == SortDirection.Descending ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id)  // Default sorting by Id
        };

        // Paging
        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalItems / selectParameter.PageSize);

        var entities = await query.Skip((selectParameter.Page - 1) * selectParameter.PageSize)
                                  .Take(selectParameter.PageSize)
                                  .ToListAsync();

        // Return paginated and sorted result with metadata
        SelectResult<T> result = new SelectResult<T>();
        result.TotalItems = totalItems;
        result.TotalPages = totalPages;
        result.CurrentPage = selectParameter.Page;
        result.PageSize = selectParameter.PageSize;
        result.Data = entities;

        return result;
    }
}