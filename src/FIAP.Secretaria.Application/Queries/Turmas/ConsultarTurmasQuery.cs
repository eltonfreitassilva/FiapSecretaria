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

public class ConsultarTurmasQuery : IConsultarTurmasQuery
{
    private readonly ITurmaRepository _turmaRepository;

    public ConsultarTurmasQuery(ITurmaRepository turmaRepository)
    {
        _turmaRepository = turmaRepository;
    }

    public async Task<Result<IPagedList<TurmaDto>>> Handle(ConsultarTurmasFilter filter, CancellationToken cancellationToken)
    {
        var result = new Result<IPagedList<TurmaDto>>();

        filter.Validate(result);

        if (result.IsValid)
        {
            var filtrosConfigurados = ConfigurarFiltros(filter);

            var dadosPaginados = await _turmaRepository.GetPagedList(filtrosConfigurados);

            var dados = dadosPaginados
                .Items
                .Select(x => (TurmaDto)x)
                .ToList();

            result.Data = new PagedList<TurmaDto>(dados, dadosPaginados.Page, dadosPaginados.TotalItems, dadosPaginados.ItemsPerPage);
        }

        return result;
    }

    private DataFilter<Turma> ConfigurarFiltros(ConsultarTurmasFilter filter)
    {
        DataFilter<Turma> filtros = new DataFilter<Turma>();
        filtros.PageIndex = filter.Pagination.PageIndex;
        filtros.PageSize = filter.Pagination.PageSize;
        filtros.SortDirection = SortDirection.Ascending;
        filtros.SortColumn = c => c.Nome;
        filtros.Predicate = PrepararConsulta(filter);

        filtros.Includes =
                    [
                          t => t.Matriculas
                    ];

        if (filter.Pagination.SortIndex >= 0)
        {
            filtros.SortDirection = filter.Pagination.SortDirection;

            if (filter.Pagination.SortIndex == 0)
            {
                filtros.SortColumn = c => c.Nome;
            }
            else if (filter.Pagination.SortIndex == 1)
            {
                filtros.SortColumn = c => c.DataCriacao;
            }
        }

        return filtros;
    }

    private Expression<Func<Turma, bool>> PrepararConsulta(ConsultarTurmasFilter filter)
    {
        if (filter == null)
            return default;

        Expression<Func<Turma, bool>> consulta = c =>

            (filter.Id == null || c.Id == filter.Id) &&
            (string.IsNullOrEmpty(filter.Nome) || c.Nome.Contains(filter.Nome)) &&
            (string.IsNullOrEmpty(filter.Descricao) || c.Descricao.Contains(filter.Descricao)) &&
            (filter.Ativas == null || c.Ativo == filter.Ativas)

        ;

        return consulta;
    }
}