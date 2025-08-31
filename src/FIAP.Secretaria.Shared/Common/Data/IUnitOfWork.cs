namespace FIAP.Secretaria.Shared.Common.Data;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
    Task RollbackAsync();
}