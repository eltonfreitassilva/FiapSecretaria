namespace FIAP.Secretaria.Shared.Common.Queries;

public interface IQueryHandler<TResult>
{
    Task<TResult> Handle(CancellationToken cancellationToken = default);
}
