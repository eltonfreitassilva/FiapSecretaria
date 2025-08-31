using FIAP.Secretaria.Domain.Entities;
using FIAP.Secretaria.Domain.Interfaces.Repositories;
using FIAP.Secretaria.Infrastructure.Repositories;

namespace FIAP.Secretaria.Infrastructure.Data.Repositories;

public class AlunoRepository : BaseRepository<Aluno>, IAlunoRepository
{
    public AlunoRepository(SecretariaContext context) : base(context)
    {
    }
}
