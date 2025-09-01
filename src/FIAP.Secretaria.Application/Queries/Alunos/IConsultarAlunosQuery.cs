using FIAP.Secretaria.Application.Dtos;
using FIAP.Secretaria.Application.Filters.Alunos;
using FIAP.Secretaria.Shared.Common.Queries;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Utils.Pagination;

namespace FIAP.Secretaria.Application.Queries.Alunos;

public interface IConsultarAlunosQuery : IQueryFilterHandler<ConsultarAlunosFilter, Result<IPagedList<AlunoDto>>> { }