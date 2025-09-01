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

    public static implicit operator AlunoDto(Aluno entity)
    {
        return new AlunoDto
        {
            Id = entity.Id,
            Nome = entity.Nome,
            DataNascimento = entity.DataNascimento,
            Cpf = entity.Cpf,
            Email = entity.Email,
            DataCriacao = entity.DataCriacao,
            DataAlteracao = entity?.DataAlteracao
        };
    }
}