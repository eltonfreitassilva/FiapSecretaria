using FIAP.Secretaria.Application.Dtos;
using FIAP.Secretaria.Application.Filters.Matriculas;
using FIAP.Secretaria.Application.Queries.Matriculas;
using FIAP.Secretaria.Shared.Common.Data;
using FIAP.Secretaria.Shared.Common.Enums;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Common.Validators;
using FIAP.Secretaria.Shared.Utils.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace FIAP.Secretaria.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = $"Administrador")]
    public class MatriculasController : DefaultController
    {
        private readonly IConsultarMatriculasQuery _consultarMatriculasQuery;

        public MatriculasController(IMediator mediator, IConsultarMatriculasQuery consultarMatriculasQuery) : base(mediator)
        {
            _consultarMatriculasQuery = consultarMatriculasQuery;
        }

        [HttpPost]
        [SwaggerOperation(summary: "Efetuar matrícula de aluno.", description: "Permite realizar a matrícula de um aluno em uma turma.")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Result<bool>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(Result<bool>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(Result<bool>))]
        public async Task<IActionResult> CadastrarMatricula([FromBody] CadastrarMatriculaCommand command)
        {
            var result = await _mediator.Send(command);
            return GetIActionResult(result);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(summary: "Obter matrícula por ID", description: "Permite obter uma matrícula específica pelo seu ID.")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Result<IPagedList<MatriculaDto>>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(Result<bool>))]
        public async Task<IActionResult> ObterMatriculaPorId([FromRoute] int id, [FromQuery] ConsultarMatriculasFilter filtros, [FromQuery] PaginationFilter paginacao)
        {
            var result = new Result<IPagedList<MatriculaDto>>();

            Validations.IsTrue(id <= 0, result, "Matricula", "ID da matrícula inválido.");

            if (result.IsValid)
            {
                if (filtros == null)
                    filtros = new ConsultarMatriculasFilter();

                if (paginacao == null)
                    paginacao = new PaginationFilter();

                paginacao.SortIndex = 0;
                paginacao.SortDirection = SortDirection.Ascending;
                filtros.Pagination = paginacao;
                filtros.Id = id;

                result = await _consultarMatriculasQuery.Handle(filtros);
            }

            return GetIActionResult(result);
        }

        [HttpGet]
        [SwaggerOperation(summary: "Listar matrículas", description: "Permite listar matrículas com filtros.")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Result<IPagedList<MatriculaDto>>))]
        public async Task<IActionResult> ListarMatriculas([FromQuery] ConsultarMatriculasFilter filtros, [FromQuery] PaginationFilter paginacao)
        {
            if (filtros == null)
                filtros = new ConsultarMatriculasFilter();

            if (paginacao == null)
                paginacao = new PaginationFilter();

            paginacao.SortIndex = 0;
            paginacao.SortDirection = SortDirection.Ascending;
            filtros.Pagination = paginacao;

            var result = await _consultarMatriculasQuery.Handle(filtros);
            return GetIActionResult(result);
        }

        [HttpGet("aluno/{alunoId:int}")]
        [SwaggerOperation(summary: "Listar matrículas por aluno", description: "Permite listar matrículas de um aluno específico.")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Result<IPagedList<MatriculaDto>>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(Result<bool>))]
        public async Task<IActionResult> ListarMatriculasPorAluno([FromRoute] int alunoId, [FromQuery] PaginationFilter paginacao)
        {
            var result = new Result<IPagedList<MatriculaDto>>();

            Validations.IsTrue(alunoId <= 0, result, "Aluno", "ID do aluno inválido.");

            if (result.IsValid)
            {
                var filtros = new ConsultarMatriculasFilter { AlunoId = alunoId };

                if (paginacao == null)
                    paginacao = new PaginationFilter();

                paginacao.SortIndex = 0;
                paginacao.SortDirection = SortDirection.Descending;
                filtros.Pagination = paginacao;

                result = await _consultarMatriculasQuery.Handle(filtros);
            }

            return GetIActionResult(result);
        }

        [HttpGet("turma/{turmaId:int}")]
        [SwaggerOperation(summary: "Listar matrículas por turma", description: "Permite listar matrículas de uma turma específica.")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Result<IPagedList<MatriculaDto>>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(Result<IPagedList<MatriculaDto>>))]
        public async Task<IActionResult> ListarMatriculasPorTurma([FromRoute] int turmaId, [FromQuery] PaginationFilter paginacao)
        {
            var result = new Result<IPagedList<MatriculaDto>>();

            Validations.IsTrue(turmaId <= 0, result, "Turma", "ID da turma inválido.");

            if (result.IsValid)
            {
                var filtros = new ConsultarMatriculasFilter { TurmaId = turmaId };

                if (paginacao == null)
                    paginacao = new PaginationFilter();

                paginacao.SortIndex = 0;
                paginacao.SortDirection = SortDirection.Ascending;
                filtros.Pagination = paginacao;

                result = await _consultarMatriculasQuery.Handle(filtros);
            }

            return GetIActionResult(result);
        }

        [HttpGet("aluno/{alunoId:int}/turma/{turmaId:int}")]
        [SwaggerOperation(summary: "Verificar matrícula", description: "Verifica se um aluno está matriculado em uma turma específica.")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Result<IPagedList<MatriculaDto>>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(Result<IPagedList<MatriculaDto>>))]
        public async Task<IActionResult> VerificarMatricula([FromRoute] int alunoId, [FromRoute] int turmaId)
        {
            var result = new Result<bool>();

            Validations.IsTrue(alunoId <= 0, result, "Aluno", "ID do aluno inválido.");
            Validations.IsTrue(turmaId <= 0, result, "Turma", "ID da turma inválido.");

            if (result.IsValid)
            {
                var filtros = new ConsultarMatriculasFilter
                {
                    AlunoId = alunoId,
                    TurmaId = turmaId
                };

                var consultaResult = await _consultarMatriculasQuery.Handle(filtros);

                if (consultaResult.IsValid)
                {
                    result.Data = consultaResult.Data?.Items?.Any() == true;
                    result.SetMessage(result.Data ? "Aluno matriculado na turma." : "Aluno não matriculado na turma.");
                }
            }

            return GetIActionResult(result);
        }
    }
}