using FIAP.Secretaria.Application.Commands.Turmas;
using FIAP.Secretaria.Domain.Interfaces.Repositories;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Common.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Secretaria.Application.CommandsHandlers;

public class DeletarTurmaCommandHandler : IRequestHandler<DeletarTurmaCommand, Result<bool>>
{
    private readonly ITurmaRepository _turmaRepository;

    public DeletarTurmaCommandHandler(ITurmaRepository turmaRepository)
    {
        _turmaRepository = turmaRepository;
    }

    public async Task<Result<bool>> Handle(DeletarTurmaCommand command, CancellationToken cancellationToken)
    {
        var result = new Result<bool>();

        command.Validate(result);

        if (result.IsValid)
        {
            var turma = await _turmaRepository.AsQueryable()
                .Where(x => x.Id == command.Id && x.Ativo == true).FirstOrDefaultAsync();

            Validations.IsNull(turma, result, "Turma", "Turma não encontrada.");

            if (result.IsValid)
            {
                turma.Deletar();

                await _turmaRepository.UnitOfWork.CommitAsync();

                result.Data = true;
                result.SetMessage("Turma excluída com sucesso.");
            }
        }

        return result;
    }
}