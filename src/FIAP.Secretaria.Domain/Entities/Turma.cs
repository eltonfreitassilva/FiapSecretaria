using FIAP.Secretaria.Shared.Common.Entities;

namespace FIAP.Secretaria.Domain.Entities;

public class Turma : Entity
{
    public string Nome { get; protected set; }
    public string Descricao { get; protected set; }

    public virtual ICollection<Matricula> Matriculas { get; protected set; }

    protected Turma() { }

    public Turma(string nome, string descricao)
    {
        Nome = nome;
        Descricao = descricao;
        Matriculas = new List<Matricula>();
    }
   
    public static class Factory
    {
        public static Turma Criar(string nome, string descricao)
        {
            return new Turma(nome, descricao);
        }
    }
}