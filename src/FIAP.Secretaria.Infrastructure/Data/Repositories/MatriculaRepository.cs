using FIAP.Secretaria.Domain.Entities;
using FIAP.Secretaria.Domain.Interfaces.Repositories;
using FIAP.Secretaria.Infrastructure.Repositories;

namespace FIAP.Secretaria.Infrastructure.Data.Repositories;

public class MatriculaRepository : BaseRepository<Matricula>, IMatriculaRepository
{
    public MatriculaRepository(SecretariaContext context) : base(context)
    {
    }
}