using FIAP.Secretaria.Domain.Interfaces.Repositories;
using FIAP.Secretaria.Infrastructure.Data;
using FIAP.Secretaria.Shared.Common.Data;
using FIAP.Secretaria.Shared.Common.Entities;
using FIAP.Secretaria.Shared.Common.Enums;
using FIAP.Secretaria.Shared.Utils.Pagination;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Secretaria.Infrastructure.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
{
    private bool _disposed = false;

    protected readonly SecretariaContext _context;
    protected readonly DbSet<T> _dbSet;

    protected BaseRepository(SecretariaContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public IUnitOfWork UnitOfWork => _context;
    public string ConnectionString => _context.Database.GetDbConnection().ConnectionString;

    public virtual async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual async Task<IPagedList<T>> GetPagedList(IDataFilter<T> filters)
    {
        if (filters == null)
            return default;

        IList<T> items = default;
        int recordsCount = 0;
        IQueryable<T> query = _context.Set<T>()
            .AsQueryable();

        if (filters.Predicate != null)
            query = query.Where(filters.Predicate);

        if (filters.SortColumn != null)
        {
            if (filters.SortDirection == SortDirection.Ascending)
                query = query.OrderBy(filters.SortColumn);
            else
                query = query.OrderByDescending(filters.SortColumn);
        }

        var data = await query
            .Select(x => new
            {
                Record = x,
                RecordsCount = query.Count()
            })
            .Skip(filters.PageStart)
            .Take(filters.PageSize)
            .ToListAsync();

        recordsCount = data?.FirstOrDefault()?.RecordsCount ?? query.Count();
        items = data?.Select(x => x.Record).ToList();

        return await Task.FromResult(new PagedList<T>(items, filters.PageIndex, recordsCount, filters.PageSize));
    }

    public virtual IQueryable<T> AsQueryable() => _context.Set<T>().AsQueryable();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {

        }

        _disposed = true; 
    }
}