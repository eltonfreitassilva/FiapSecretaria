using FIAP.Secretaria.Shared.Common.Data;
using FIAP.Secretaria.Shared.Common.Entities;
using FIAP.Secretaria.Shared.Utils.Pagination;

namespace FIAP.Secretaria.Domain.Interfaces.Repositories;

public interface IBaseRepository<T> : IDisposable where T : Entity
{
    public IUnitOfWork UnitOfWork { get; }
    string ConnectionString { get; }
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<IPagedList<T>> GetPagedList(IDataFilter<T> filters);
    IQueryable<T> AsQueryable();
}