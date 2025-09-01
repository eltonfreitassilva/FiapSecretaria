using FIAP.Secretaria.Domain.Interfaces.Repositories;
using FIAP.Secretaria.Infrastructure.Services;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Common.Validators;
using FIAP.Secretaria.Shared.Utils.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Secretaria.Application.CommandsHandlers.Alunos;

public class CadastrarAlunoCommandHandler : IRequestHandler<CadastrarAlunoCommand, Result<bool>>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAlunoRepository _alunoRepository;

    public CadastrarAlunoCommandHandler(IPasswordHasher passwordHasher, IAlunoRepository alunoRepository)
    {
        _passwordHasher = passwordHasher;
        _alunoRepository = alunoRepository;
    }

    public async Task<Result<bool>> Handle(CadastrarAlunoCommand command, CancellationToken cancellationToken)
    {
        var result = new Result<bool>();

        command.Validate(result);

        if (result.IsValid)
        {
            var cpf = CpfHelper.RemoverFormatacaoCpf(command.Cpf);

            Validations.IsTrue(string.IsNullOrWhiteSpace(cpf), result, "Cpf", "cpf inválido.");

            if (result.IsValid)
            {
                var alunoExistente = await _alunoRepository
                     .AsQueryable()
                     .Where(a => a.Cpf == command.Cpf || a.Email == command.Email)
                     .Select(a => new { a.Cpf, a.Email })
                     .FirstOrDefaultAsync(cancellationToken);

                Validations.IsNotNull(alunoExistente?.Cpf, result, "Cpf", "Já existe um aluno cadastrado com o CPF informado.");
                Validations.IsNotNull(alunoExistente?.Email, result, "Email", "Já existe um aluno cadastrado com o e-mail informado.");

                if (result.IsValid)
                {
                    var senhaHash = _passwordHasher.HashPassword(command.Senha);

                    var aluno = Domain.Entities.Aluno.Factory.Criar(command.Nome, command.DataNascimento, command.Cpf, command.Email, senhaHash);

                    await _alunoRepository.AddAsync(aluno);
                    await _alunoRepository.UnitOfWork.CommitAsync();

                    result.Data = true;
                    result.SetMessage("Aluno cadastrado com sucesso.");
                }
            }

        }

        return result;
    }
}