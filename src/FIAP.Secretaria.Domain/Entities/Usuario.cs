using FIAP.Secretaria.Domain.Enums;
using FIAP.Secretaria.Shared.Common.Entities;

namespace FIAP.Secretaria.Domain.Entities;

public class Usuario : Entity
{
    public string Email { get; private set; }
    public string Senha { get; private set; }
    public PerfilUsuario Perfil { get; private set; }

    protected Usuario() { }

    public Usuario(string email, string senhaHash, PerfilUsuario perfil)
    {
        Email = email;
        Senha = senhaHash;
        Perfil = perfil;
    }

    public void Atualizar(string email, PerfilUsuario perfil)
    {
        Email = email;
        Perfil = perfil;
    }
}