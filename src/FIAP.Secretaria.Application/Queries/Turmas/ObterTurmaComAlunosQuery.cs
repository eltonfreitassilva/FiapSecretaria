using FIAP.Secretaria.Application.Dtos;
using FIAP.Secretaria.Domain.Interfaces.Repositories;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Common.Validators;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Secretaria.Application.Queries.Turmas
{
    public class ObterTurmaComAlunosQuery : IObterTurmaComAlunosQuery
    {
        private readonly ITurmaRepository _turmaRepository;
        public ObterTurmaComAlunosQuery(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;
        }

        public async Task<Result<TurmaComAlunosDto>> Handle(int turmaId)
        {
            var result = new Result<TurmaComAlunosDto>();

            var turma = await _turmaRepository.AsQueryable()
                .Include(t => t.Matriculas)
                .ThenInclude(m => m.Aluno)
                .FirstOrDefaultAsync(t => t.Id == turmaId);

            Validations.IsNull(turma, result, "Turma", "Turma não encontrada.");

            if (result.IsValid)
            {
                var turmaComAlunos = new TurmaComAlunosDto
                {
                    Id = turma.Id,
                    Nome = turma.Nome,
                    Descricao = turma.Descricao,
                    DataCriacao = turma.DataCriacao,
                    DataAlteracao = turma.DataAlteracao,
                    QuantidadeAlunos = turma.Matriculas.Count,
                    Alunos = turma.Matriculas
                        .Select(m => (AlunoMatriculaDto)m)
                            .ToList()
                  };

                result.Data = turmaComAlunos;
            }

            return result;
        }
    }
}
