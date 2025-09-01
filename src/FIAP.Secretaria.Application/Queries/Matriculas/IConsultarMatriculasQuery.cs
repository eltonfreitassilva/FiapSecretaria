using FIAP.Secretaria.Application.Dtos;
using FIAP.Secretaria.Application.Filters.Matriculas;
using FIAP.Secretaria.Shared.Common.Queries;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Utils.Pagination;

namespace FIAP.Secretaria.Application.Queries.Matriculas;

public interface IConsultarMatriculasQuery : IQueryFilterHandler<ConsultarMatriculasFilter, Result<IPagedList<MatriculaDto>>> { }