using FIAP.Secretaria.Domain.Entities;

namespace FIAP.Secretaria.Application.Dtos;

public class AlunoTurmaDto
{
    public int AlunoId { get; set; }
    public string NomeAluno { get; set; }
    public string EmailAluno { get; set; }
    public string CpfAluno { get; set; }
    public DateTime DataNascimentoAluno { get; set; }
    public DateTime DataMatricula { get; set; }
    public bool Ativo { get; set; }

    public static implicit operator AlunoTurmaDto(Matricula matricula)
    {
        if (matricula?.Aluno == null)
            return null;

        return new AlunoTurmaDto
        {
            AlunoId = matricula.Aluno.Id,
            NomeAluno = matricula.Aluno.Nome,
            EmailAluno = matricula.Aluno.Email,
            CpfAluno = matricula.Aluno.Cpf,
            DataNascimentoAluno = matricula.Aluno.DataNascimento,
            DataMatricula = matricula.DataMatricula,
            Ativo = matricula.Aluno.Ativo
        };
    }
}