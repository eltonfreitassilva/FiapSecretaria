using FIAP.Secretaria.Shared.Common.Entities;
using FIAP.Secretaria.Shared.Common.Enums;
using System.Linq.Expressions;

namespace FIAP.Secretaria.Shared.Common.Data;

public interface IDataFilter<T> where T : IEntity
{
    int PageIndex { get; set; }
    int PageSize { get; set; }
    int PageStart { get; }
    Expression<Func<T, object>> SortColumn { get; set; }
    SortDirection SortDirection { get; set; }
    Expression<Func<T, bool>> Predicate { get; set; }
    List<Expression<Func<T, object>>> Includes { get; set; }
}
