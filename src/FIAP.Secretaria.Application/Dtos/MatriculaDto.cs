using FIAP.Secretaria.Domain.Entities;

namespace FIAP.Secretaria.Application.Dtos;

public class MatriculaDto
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public int TurmaId { get; set; }
    public DateTime DataMatricula { get; set; }
    public string AlunoNome { get; set; }
    public string TurmaNome { get; set; }

    public static implicit operator MatriculaDto(Matricula matricula)
    {
        if (matricula == null) return null;

        return new MatriculaDto
        {
            Id = matricula.Id,
            AlunoId = matricula.AlunoId,
            TurmaId = matricula.TurmaId,
            DataMatricula = matricula.DataMatricula,
            AlunoNome = matricula?.Aluno?.Nome,
            TurmaNome = matricula?.Turma?.Nome
        };
    }
}