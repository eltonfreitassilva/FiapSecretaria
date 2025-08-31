namespace FIAP.Secretaria.Shared.Common.Models;

public class UsuarioAutenticadoModel
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Perfil { get; private set; }
    public List<string> Roles { get; private set; }

    public UsuarioAutenticadoModel(int id, string nome, string email, string perfil)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Perfil = perfil;
        Roles = new List<string> { perfil };
    }

    public bool IsInRole(string role) => Roles.Contains(role);
    public bool IsAdmin() => Perfil == "Administrador";
}