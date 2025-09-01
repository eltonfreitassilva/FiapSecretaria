using FIAP.Secretaria.Application.Dtos;
using FIAP.Secretaria.Application.Filters.Matriculas;
using FIAP.Secretaria.Domain.Entities;
using FIAP.Secretaria.Domain.Interfaces.Repositories;
using FIAP.Secretaria.Shared.Common.Data;
using FIAP.Secretaria.Shared.Common.Enums;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Utils.Pagination;
using System.Linq.Expressions;

namespace FIAP.Secretaria.Application.Queries.Matriculas;

public class ConsultarMatriculasQuery : IConsultarMatriculasQuery
{
    private readonly IMatriculaRepository _matriculaRepository;

    public ConsultarMatriculasQuery(IMatriculaRepository matriculaRepository)
    {
        _matriculaRepository = matriculaRepository;
    }

    public async Task<Result<IPagedList<MatriculaDto>>> Handle(ConsultarMatriculasFilter filter, CancellationToken cancellationToken)
    {
        var result = new Result<IPagedList<MatriculaDto>>();

        filter.Validate(result);

        if (result.IsValid)
        {
            var filtrosConfigurados = ConfigurarFiltros(filter);

            var dadosPaginados = await _matriculaRepository.GetPagedList(filtrosConfigurados);

            var dados = dadosPaginados
                .Items
                .Select(x => (MatriculaDto)x)
                .ToList();

            result.Data = new PagedList<MatriculaDto>(dados, dadosPaginados.Page, dadosPaginados.TotalItems, dadosPaginados.ItemsPerPage);
        }

        return result;
    }

    private DataFilter<Matricula> ConfigurarFiltros(ConsultarMatriculasFilter filter)
    {
        DataFilter<Matricula> filtros = new DataFilter<Matricula>();
        filtros.PageIndex = filter.Pagination.PageIndex;
        filtros.PageSize = filter.Pagination.PageSize;
        filtros.SortDirection = SortDirection.Ascending;
        filtros.SortColumn = c => c.Turma.Nome;
        filtros.Predicate = PrepararConsulta(filter);

        filtros.Includes =
                    [
                          m => m.Aluno,
                          m => m.Turma
                    ];


        if (filter.Pagination.SortIndex >= 0)
        {
            filtros.SortDirection = filter.Pagination.SortDirection;

            if (filter.Pagination.SortIndex == 0)
            {
                filtros.SortColumn = c => c.Turma.Nome;
            }
        }

        return filtros;
    }

    private Expression<Func<Matricula, bool>> PrepararConsulta(ConsultarMatriculasFilter filter)
    {
        if (filter == null)
            return default;

        Expression<Func<Matricula, bool>> consulta = c =>

            filter.Id == null || c.Id == filter.Id;

        return consulta;
    }
}