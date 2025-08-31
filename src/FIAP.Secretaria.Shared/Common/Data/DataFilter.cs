using FIAP.Secretaria.Shared.Common.Entities;
using FIAP.Secretaria.Shared.Common.Enums;
using System.Linq.Expressions;

namespace FIAP.Secretaria.Shared.Common.Data;

public class DataFilter<T> : IDataFilter<T> where T : IEntity
{
    private int _pageIndex = 1;
    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = (value <= 0) ? 1 : value;
    }

    private int _pageSize = 50;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value <= 0 || value > 100) ? 50 : value;
    }

    public int PageStart => (PageIndex - 1) * PageSize;
    public Expression<Func<T, object>> SortColumn { get; set; }
    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
    public Expression<Func<T, bool>> Predicate { get; set; }
    public Expression<Func<T, object>> Include { get; set; }
}
