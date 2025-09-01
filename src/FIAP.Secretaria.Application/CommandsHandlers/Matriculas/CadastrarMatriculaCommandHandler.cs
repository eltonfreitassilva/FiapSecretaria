using FIAP.Secretaria.Domain.Interfaces.Repositories;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Common.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Secretaria.Application.CommandsHandlers;

public class CadastrarMatriculaCommandHandler : IRequestHandler<CadastrarMatriculaCommand, Result<bool>>
{
    private readonly IMatriculaRepository _matriculaRepository;
    private readonly IAlunoRepository _alunoRepository;
    private readonly ITurmaRepository _turmaRepository;

    public CadastrarMatriculaCommandHandler(
        IMatriculaRepository matriculaRepository,
        IAlunoRepository alunoRepository,
        ITurmaRepository turmaRepository)
    {
        _matriculaRepository = matriculaRepository;
        _alunoRepository = alunoRepository;
        _turmaRepository = turmaRepository;
    }

    public async Task<Result<bool>> Handle(CadastrarMatriculaCommand command, CancellationToken cancellationToken)
    {
        var result = new Result<bool>();

        command.Validate(result);

        if (result.IsValid)
        {
            var aluno = await _alunoRepository.AsQueryable()
                .Where(x => x.Id == command.AlunoId).FirstOrDefaultAsync();

            Validations.IsNull(aluno, result, "Aluno", "Aluno não encontrado.");

            var turma = await _turmaRepository.AsQueryable()
                .Where(x => x.Id == command.TurmaId).FirstOrDefaultAsync();

            Validations.IsNull(turma, result, "Turma", "Turma não encontrada.");

            if (result.IsValid)
            {
                var matriculaExistente = await _matriculaRepository
                    .AsQueryable()
                    .Where(m => m.AlunoId == command.AlunoId && m.TurmaId == command.TurmaId)
                    .FirstOrDefaultAsync(cancellationToken);

                Validations.IsNotNull(matriculaExistente, result, "Matricula", "Aluno já está matriculado nesta turma.");

                if (result.IsValid)
                {
                    var matricula = Domain.Entities.Matricula.Factory.Criar(command.AlunoId, command.TurmaId);

                    await _matriculaRepository.AddAsync(matricula);

                    await _matriculaRepository.UnitOfWork.CommitAsync();

                    result.Data = true;
                    result.SetMessage("Matrícula realizada com sucesso.");
                }
            }
        }

        return result;
    }
}