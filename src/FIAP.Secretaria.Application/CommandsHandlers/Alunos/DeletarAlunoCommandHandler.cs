using FIAP.Secretaria.Domain.Interfaces.Repositories;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Common.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class DeletarAlunoCommandHandler : IRequestHandler<DeletarAlunoCommand, Result<bool>>
{
    private readonly IAlunoRepository _alunoRepository;

    public DeletarAlunoCommandHandler(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    public async Task<Result<bool>> Handle(DeletarAlunoCommand command, CancellationToken cancellationToken)
    {
        var result = new Result<bool>();
        command.Validate(result);


        if (result.IsValid)
        {
            var aluno = await _alunoRepository.AsQueryable()
            .Where(x => x.Id == command.Id).FirstOrDefaultAsync();

            Validations.IsNull(aluno, result, "Aluno", "Aluno não encontrado");

            if (result.IsValid)
            {
                _alunoRepository.Delete(aluno);

                await _alunoRepository.UnitOfWork.CommitAsync();

                result.Data = true;
                result.SetMessage("Aluno deletado com sucesso.");
            }
        }
        
        return result;
    }
}