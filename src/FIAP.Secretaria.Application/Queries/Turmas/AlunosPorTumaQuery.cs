using FIAP.Secretaria.Application.Dtos;
using FIAP.Secretaria.Application.Filters.Turmas;
using FIAP.Secretaria.Domain.Entities;
using FIAP.Secretaria.Domain.Interfaces.Repositories;
using FIAP.Secretaria.Shared.Common.Data;
using FIAP.Secretaria.Shared.Common.Enums;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Utils.Pagination;
using System.Linq.Expressions;

namespace FIAP.Secretaria.Application.Queries.Turmas;

public class AlunosPorTumaQuery : IAlunosPorTumaQuery
{
    private readonly ITurmaRepository _turmaRepository;
    private readonly IMatriculaRepository _matriculaRepository;

    public AlunosPorTumaQuery(ITurmaRepository turmaRepository, IMatriculaRepository matriculaRepository)
    {
        _turmaRepository = turmaRepository;
        _matriculaRepository = matriculaRepository;
    }

    public async Task<Result<IPagedList<AlunoTurmaDto>>> Handle(AlunosPorTumaFilter filter, CancellationToken cancellationToken = default)
    {
        var result = new Result<IPagedList<AlunoTurmaDto>>();

        filter.Validate(result);

        if (result.IsValid)
        {
            var filtrosConfigurados = ConfigurarFiltros(filter);

            var dadosPaginados = await _matriculaRepository.GetPagedList(filtrosConfigurados);

            var dados = dadosPaginados
                .Items
                .Select(x => (AlunoTurmaDto)x)
                .ToList();

            result.Data = new PagedList<AlunoTurmaDto>(dados, dadosPaginados.Page, dadosPaginados.TotalItems, dadosPaginados.ItemsPerPage);
        }

        return result;
    }

    private DataFilter<Matricula> ConfigurarFiltros(AlunosPorTumaFilter filter)
    {
        DataFilter<Matricula> filtros = new DataFilter<Matricula>();
        filtros.PageIndex = filter.Pagination.PageIndex;
        filtros.PageSize = filter.Pagination.PageSize;
        filtros.SortDirection = SortDirection.Ascending;
        filtros.SortColumn = c => c.Aluno.Nome;
        filtros.Predicate = PrepararConsulta(filter);

        filtros.Includes =
                    [
                          t => t.Aluno
                    ];

        if (filter.Pagination.SortIndex >= 0)
        {
            filtros.SortDirection = filter.Pagination.SortDirection;

            if (filter.Pagination.SortIndex == 0)
            {
                filtros.SortColumn = c => c.Aluno.Nome;
            }
        }

        return filtros;
    }

    private Expression<Func<Matricula, bool>> PrepararConsulta(AlunosPorTumaFilter filter)
    {
        if (filter == null)
            return default;

        Expression<Func<Matricula, bool>> consulta = c =>

            (filter.Id == null || c.TurmaId == filter.Id);

        return consulta;
    }
}