using FIAP.Secretaria.Domain.Interfaces.Repositories;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Common.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Secretaria.Application.CommandsHandlers;

public class EditarTurmaCommandHandler : IRequestHandler<EditarTurmaCommand, Result<bool>>
{
    private readonly ITurmaRepository _turmaRepository;

    public EditarTurmaCommandHandler(ITurmaRepository turmaRepository)
    {
        _turmaRepository = turmaRepository;
    }

    public async Task<Result<bool>> Handle(EditarTurmaCommand command, CancellationToken cancellationToken)
    {
        var result = new Result<bool>();

        command.Validate(result);

        if (result.IsValid)
        {
            var turma = await _turmaRepository.AsQueryable()
                .Where(x=> x.Id == command.Id).FirstOrDefaultAsync();

            Validations.IsNull(turma, result, "Turma", "Turma não encontrada.");

            if (result.IsValid)
            {
                var turmaComMesmoNome = await _turmaRepository
                    .AsQueryable()
                    .Where(t => t.Nome == command.Nome && t.Id != command.Id)
                    .Select(t => new { t.Nome })
                    .FirstOrDefaultAsync(cancellationToken);

                Validations.IsNotNull(turmaComMesmoNome?.Nome, result, "Nome", "Já existe uma turma cadastrada com o nome informado.");

                if (result.IsValid)
                {
                    turma.Atualizar(command.Nome, command.Descricao);

                    _turmaRepository.Update(turma);

                    await _turmaRepository.UnitOfWork.CommitAsync();

                    result.Data = true;
                    result.SetMessage("Turma atualizada com sucesso.");
                }
            }
        }

        return result;
    }
}