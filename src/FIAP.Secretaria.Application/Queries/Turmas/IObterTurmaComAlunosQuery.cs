using FIAP.Secretaria.Application.Dtos;
using FIAP.Secretaria.Shared.Common.Results;

namespace FIAP.Secretaria.Application.Queries.Turmas;

public interface IObterTurmaComAlunosQuery
{
    Task<Result<TurmaComAlunosDto>> Handle(int turmaId);
}
