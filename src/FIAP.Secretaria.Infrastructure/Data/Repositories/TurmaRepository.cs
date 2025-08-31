using FIAP.Secretaria.Domain.Entities;
using FIAP.Secretaria.Domain.Interfaces.Repositories;
using FIAP.Secretaria.Infrastructure.Data;

namespace FIAP.Secretaria.Infrastructure.Repositories;

public class TurmaRepository : BaseRepository<Turma>, ITurmaRepository
{
    private readonly SecretariaContext _context;

    public TurmaRepository(SecretariaContext context) : base(context)
    {
        _context = context;
    }
}