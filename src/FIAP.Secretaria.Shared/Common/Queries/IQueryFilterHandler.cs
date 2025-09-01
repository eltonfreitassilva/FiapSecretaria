namespace FIAP.Secretaria.Shared.Common.Queries;

public interface IQueryFilterHandler<in TFilter, TResult> where TFilter : IQueryFilter<TResult>
{
    Task<TResult> Handle(TFilter filter, CancellationToken cancellationToken = default);
}
