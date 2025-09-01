using FIAP.Secretaria.Domain.Interfaces.Repositories;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Common.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Secretaria.Application.CommandsHandlers;

public class CadastrarTurmaCommandHandler : IRequestHandler<CadastrarTurmaCommand, Result<bool>>
{
    private readonly ITurmaRepository _turmaRepository;

    public CadastrarTurmaCommandHandler(ITurmaRepository turmaRepository)
    {
        _turmaRepository = turmaRepository;
    }

    public async Task<Result<bool>> Handle(CadastrarTurmaCommand command, CancellationToken cancellationToken)
    {
        var result = new Result<bool>();

        command.Validate(result);

        if (result.IsValid)
        {
            var turmaExistente = await _turmaRepository
                .AsQueryable()
                .AsNoTracking()
                .Where(t => t.Nome == command.Nome)
                .Select(t => new { t.Nome })
                .FirstOrDefaultAsync(cancellationToken);

            Validations.IsNotNull(turmaExistente?.Nome, result, "Nome", "Já existe uma turma cadastrada com o nome informado.");

            if (result.IsValid)
            {
                var turma = Domain.Entities.Turma.Factory.Criar(command.Nome, command.Descricao);

                await _turmaRepository.AddAsync(turma);
                await _turmaRepository.UnitOfWork.CommitAsync();

                result.Data = true;
                result.SetMessage("Turma cadastrada com sucesso.");
            }
        }

        return result;
    }
}