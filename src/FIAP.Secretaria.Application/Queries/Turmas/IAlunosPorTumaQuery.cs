using FIAP.Secretaria.Application.Dtos;
using FIAP.Secretaria.Application.Filters.Turmas;
using FIAP.Secretaria.Shared.Common.Queries;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Utils.Pagination;

namespace FIAP.Secretaria.Application.Queries.Turmas;

public interface IAlunosPorTumaQuery : IQueryFilterHandler<AlunosPorTumaFilter, Result<IPagedList<AlunoTurmaDto>>>
{
}
