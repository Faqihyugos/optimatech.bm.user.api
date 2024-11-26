using Microsoft.EntityFrameworkCore;
using OptimaTech.BuildingManager.User.Application;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Infrastructure.Repositories;

public abstract class RepositoryBase<T> : IRepository<T> where T : class, IApplicationModel
{
    protected readonly AppDbContext _context;
    public RepositoryBase(AppDbContext context)
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

        // Apply dynamic filtering based on the query parameters
        if (!string.IsNullOrEmpty(selectParameter.Name))
        {
            query = query.Where(p => p.Name.Contains(selectParameter.Name));
        }

        if (!string.IsNullOrEmpty(selectParameter.Code))
        {
            query = query.Where(p => p.Code == selectParameter.Code);
        }

        // Dynamic sorting
        query = selectParameter.SortBy switch
        {
            "code" => selectParameter.SortDirection == SortDirection.Descending ? query.OrderByDescending(p => p.Code) : query.OrderBy(p => p.Code),
            "name" => selectParameter.SortDirection == SortDirection.Descending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
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