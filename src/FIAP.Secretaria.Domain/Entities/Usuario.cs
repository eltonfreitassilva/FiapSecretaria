using FIAP.Secretaria.Domain.Enums;
using FIAP.Secretaria.Shared.Common.Entities;

namespace FIAP.Secretaria.Domain.Entities;

public class Usuario : Entity
{
    protected Usuario() { }

    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Senha { get; private set; }
    public PerfilUsuario Perfil { get; private set; }
    
    public static class Factory
    {
        public static Usuario Criar(string nome, string email, string senhaHash, PerfilUsuario perfil)
        {
            return new Usuario
            {
                Nome = nome,
                Email = email,
                Senha = senhaHash,
                Perfil = perfil
            };
        }
    }
}