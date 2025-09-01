using FIAP.Secretaria.Shared.Common.Entities;

namespace FIAP.Secretaria.Domain.Entities;

public class Turma : Entity
{
    public string Nome { get; protected set; }
    public string Descricao { get; protected set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime DataAlteracao { get; private set; }

    public virtual ICollection<Matricula> Matriculas { get; protected set; }

    public static class Factory
    {
        public static Turma Criar(string nome, string descricao)
        {
            return new Turma
            {
                Nome = nome,
                Descricao = descricao,
                DataCriacao = DateTime.Now
            };
        }
    }
}