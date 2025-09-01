using FIAP.Secretaria.Domain.Entities;

namespace FIAP.Secretaria.Application.Dtos;

public class AlunoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } 
    public DateTime DataNascimento { get; set; }
    public string Cpf { get; set; } 
    public string Email { get; set; } 
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public bool Ativo { get; set; }

    public static implicit operator AlunoDto(Aluno aluno)
    {
        if (aluno == null) return null;

        return new AlunoDto
        {
            Id = aluno.Id,
            Nome = aluno.Nome,
            DataNascimento = aluno.DataNascimento,
            Cpf = aluno.Cpf,
            Email = aluno.Email,
            DataCriacao = aluno.DataCriacao,
            DataAlteracao = aluno?.DataAlteracao,
            Ativo = aluno.Ativo
        };
    }
}