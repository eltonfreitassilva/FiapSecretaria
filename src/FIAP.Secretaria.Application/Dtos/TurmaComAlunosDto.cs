using FIAP.Secretaria.Domain.Entities;

namespace FIAP.Secretaria.Application.Dtos
{
    public class TurmaComAlunosDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int QuantidadeAlunos { get; set; }
        public List<AlunoMatriculaDto> Alunos { get; set; } = new List<AlunoMatriculaDto>();
    }

    public class AlunoMatriculaDto
    {
        public int AlunoId { get; set; }
        public string NomeAluno { get; set; }
        public string EmailAluno { get; set; }
        public string CpfAluno { get; set; }
        public DateTime DataNascimentoAluno { get; set; }
        public DateTime DataMatricula { get; set; }

        public static implicit operator AlunoMatriculaDto(Matricula matricula)
        {
            if (matricula?.Aluno == null)
                return null;

            return new AlunoMatriculaDto
            {
                AlunoId = matricula.Aluno.Id,
                NomeAluno = matricula.Aluno.Nome,
                EmailAluno = matricula.Aluno.Email,
                CpfAluno = matricula.Aluno.Cpf,
                DataNascimentoAluno = matricula.Aluno.DataNascimento,
                DataMatricula = matricula.DataMatricula
            };
        }
    }
}