using FIAP.Secretaria.Application.Dtos;
using FIAP.Secretaria.Application.Filters.Alunos;
using FIAP.Secretaria.Domain.Entities;
using FIAP.Secretaria.Domain.Interfaces.Repositories;
using FIAP.Secretaria.Shared.Common.Data;
using FIAP.Secretaria.Shared.Common.Enums;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Utils.Pagination;
using System.Linq.Expressions;

namespace FIAP.Secretaria.Application.Queries.Alunos;

public class ConsultarAlunosQuery : IConsultarAlunosQuery
{
    private readonly IAlunoRepository _alunoRepository;

    public ConsultarAlunosQuery(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    public async Task<Result<IPagedList<AlunoDto>>> Handle(ConsultarAlunosFilter filter, CancellationToken cancellationToken)
    {
        var result = new Result<IPagedList<AlunoDto>>();

        filter.Validate(result);

        if (result.IsValid)
        {
            var filtrosConfigurados = ConfigurarFiltros(filter);
            var dadosPaginados = await _alunoRepository.GetPagedList(filtrosConfigurados);

            var dados = dadosPaginados
                .Items
                .Select(x => (AlunoDto)x)
                .ToList();

            result.Data = new PagedList<AlunoDto>(dados, dadosPaginados.Page, dadosPaginados.TotalItems, dadosPaginados.ItemsPerPage);
        }

        return result;
    }

    private DataFilter<Aluno> ConfigurarFiltros(ConsultarAlunosFilter filter)
    {
        DataFilter<Aluno> filtros = new DataFilter<Aluno>();
        filtros.PageIndex = filter.Pagination.PageIndex;
        filtros.PageSize = filter.Pagination.PageSize;
        filtros.SortDirection = SortDirection.Ascending;
        filtros.SortColumn = c => c.Nome;
        filtros.Predicate = PrepararConsulta(filter);

        if (filter.Pagination.SortIndex >= 0)
        {
            filtros.SortDirection = filter.Pagination.SortDirection;

            if (filter.Pagination.SortIndex == 0)
            {
                filtros.SortColumn = c => c.Nome;
            }
        }

        return filtros;
    }

    private Expression<Func<Aluno, bool>> PrepararConsulta(ConsultarAlunosFilter filter)
    {
        if (filter == null)
            return default;

        Expression<Func<Aluno, bool>> consulta = c =>
            (filter.Id == null || c.Id == filter.Id) &&
            (string.IsNullOrEmpty(filter.Nome) || c.Nome.ToLower().Contains(filter.Nome.ToLower()));

        return consulta;
    }
}