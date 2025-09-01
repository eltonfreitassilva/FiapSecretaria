using FIAP.Secretaria.Domain.Entities;

namespace FIAP.Secretaria.Application.Dtos;

public class TurmaDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public int QuantidadeAlunos { get; set; }
    public bool Ativa { get; set; }

    public static implicit operator TurmaDto(Turma turma)
    {
        if (turma == null) return null;

        return new TurmaDto
        {
            Id = turma.Id,
            Nome = turma.Nome,
            Descricao = turma.Descricao,
            DataCriacao = turma.DataCriacao,
            DataAlteracao = turma.DataAlteracao,
            QuantidadeAlunos = turma.Matriculas?.Count ?? 0,
            Ativa = turma.Ativo
        };
    }
}