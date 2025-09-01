using FIAP.Secretaria.Shared.Common.Entities;

namespace FIAP.Secretaria.Domain.Entities;

public class Aluno : Entity
{
    protected Aluno() { }

    public string Nome { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public string Cpf { get; private set; }
    public string Email { get; private set; }
    public string Senha { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime? DataAlteracao { get; private set; }

    public virtual ICollection<Matricula> Matriculas { get; private set; }

    public void Atualizar(string nome, DateTime dataNascimento, string cpf, string email)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        Cpf = cpf;
        Email = email;
        DataAlteracao = DateTime.Now;
    }

    public static class Factory
    {
        public static Aluno Criar(string nome, DateTime dataNascimento, string cpf, string email, string senhaHash)
        {
            var aluno = new Aluno
            {
                Nome = nome,
                DataNascimento = dataNascimento,
                Cpf = cpf,
                Email = email,
                Senha = senhaHash,
                DataCriacao = DateTime.Now
            };

            return aluno;
        }
    }

}
