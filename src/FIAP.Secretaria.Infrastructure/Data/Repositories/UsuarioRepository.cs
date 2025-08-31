using FIAP.Secretaria.Domain.Entities;
using FIAP.Secretaria.Domain.Interfaces.Repositories;
using FIAP.Secretaria.Infrastructure.Repositories;

namespace FIAP.Secretaria.Infrastructure.Data.Repositories;

public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(SecretariaContext context) : base(context)
    {
    }
}
