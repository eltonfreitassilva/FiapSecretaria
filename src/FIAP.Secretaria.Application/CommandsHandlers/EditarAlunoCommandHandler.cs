using MediatR;
using FIAP.Secretaria.Domain.Interfaces.Repositories;
using FIAP.Secretaria.Shared.Common.Results;
using Microsoft.EntityFrameworkCore;
using FIAP.Secretaria.Shared.Common.Validators;

namespace FIAP.Secretaria.Application.Commands.Alunos;

public class EditarAlunoCommandHandler : IRequestHandler<EditarAlunoCommand, Result<bool>>
{
    private readonly IAlunoRepository _alunoRepository;

    public EditarAlunoCommandHandler(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    public async Task<Result<bool>> Handle(EditarAlunoCommand command, CancellationToken cancellationToken)
    {
        var result = new Result<bool>();
        command.Validate(result);

        if (result.IsValid)
        {
            var aluno = await _alunoRepository.AsQueryable()
                .Where(x => x.Id == command.Id).FirstOrDefaultAsync();

            Validations.IsNull(aluno, result, "Aluno", "Aluno não encontrado.");

            if (result.IsValid)
            {
                var alunoExistente = await _alunoRepository
                     .AsQueryable()
                     .Where(a => (a.Cpf == command.Cpf || a.Email == command.Email ) && a.Id != command.Id)
                     .Select(a => new { a.Cpf, a.Email })
                     .FirstOrDefaultAsync(cancellationToken);

                Validations.IsNotNull(alunoExistente?.Cpf, result, "Cpf", "Já existe um aluno cadastrado com o CPF informado.");
                Validations.IsNotNull(alunoExistente?.Email, result, "Email", "Já existe um aluno cadastrado com o e-mail informado.");

                if (result.IsValid)
                {
                    aluno.Atualizar(command.Nome, command.DataNascimento, command.Cpf, command.Email);

                    _alunoRepository.Update(aluno);

                    await _alunoRepository.UnitOfWork.CommitAsync();

                    result.Data = true;
                    result.SetMessage("Aluno atualizado com sucesso.");
                }
            }
        }

        return result;
    }
}