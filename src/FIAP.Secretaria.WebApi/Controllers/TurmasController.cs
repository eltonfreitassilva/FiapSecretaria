using FIAP.Secretaria.Application.Commands.Turmas;
using FIAP.Secretaria.Application.Dtos;
using FIAP.Secretaria.Application.Filters.Turmas;
using FIAP.Secretaria.Application.Queries.Turmas;
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
    public class TurmasController : DefaultController
    {
         private readonly IConsultarTurmasQuery _consultarTurmasQuery;
         private readonly IObterTurmaComAlunosQuery _obterTurmaComAlunosQuery;
         private readonly IAlunosPorTumaQuery _alunosPorTumaQuery;

        public TurmasController(IMediator mediator, IConsultarTurmasQuery consultarTurmasQuery, IObterTurmaComAlunosQuery obterTurmaComAlunosQuery, IAlunosPorTumaQuery alunosPorTumaQuery) : base(mediator)
        {
            _consultarTurmasQuery = consultarTurmasQuery;
            _obterTurmaComAlunosQuery = obterTurmaComAlunosQuery;
            _alunosPorTumaQuery = alunosPorTumaQuery;
        }

        [HttpPost]
        [SwaggerOperation(summary: "Efetuar cadastro de uma turma.", description: "Permite realizar o cadastro de uma turma.")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Result<bool>))]
        public async Task<IActionResult> CadastrarTurma([FromBody] CadastrarTurmaCommand command)
        {
            var result = await _mediator.Send(command);
            return GetIActionResult(result);
        }

        [HttpPut("{id:int}")]
        [SwaggerOperation(summary: "Editar o cadastro da turma.", description: "Permite editar o cadastro de uma turma.")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Result<bool>))]
        public async Task<IActionResult> EditarTurma([FromRoute] int id, [FromBody] EditarTurmaCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return GetIActionResult(result);
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(summary: "Deletar o cadastro da turma.", description: "Permite deletar o cadastro de uma turma.")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Result<bool>))]
        public async Task<IActionResult> DeletarTurma([FromRoute] int id)
        {
            var command = new DeletarTurmaCommand { Id = id };

            var result = await _mediator.Send(command);
            return GetIActionResult(result);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(summary: "Obter turma por id", description: "Permite obter uma turma por id.")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Result<TurmaDto>))]
        public async Task<IActionResult> ObterTurmaPorId([FromRoute] int id)
        {
            var filtros = new ConsultarTurmasFilter { Id = id };
            var result = await _consultarTurmasQuery.Handle(filtros);

            return GetIActionResult(result);
        }

        [HttpGet]
        [SwaggerOperation(summary: "Listar turmas", description: "Permite listar turmas.")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Result<IPagedList<TurmaDto>>))]
        public async Task<IActionResult> ListarTurmas([FromQuery] ConsultarTurmasFilter filtros, [FromQuery] PaginationFilter paginacao)
        {
            if (filtros == null)
                filtros = new ConsultarTurmasFilter();

            if (paginacao == null)
                paginacao = new PaginationFilter();

            paginacao.SortIndex = 0;
            paginacao.SortDirection = SortDirection.Ascending;
            filtros.Pagination = paginacao;

            var result = await _consultarTurmasQuery.Handle(filtros);
            return GetIActionResult(result);
        }

        [HttpGet("{idTurma:int}/Alunos")]
        [SwaggerOperation(summary: "Listar turmas e seus alunos", description: "Permite listar turmas e seus alunos.")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Result<Result<IPagedList<AlunoTurmaDto>>>))]
        public async Task<IActionResult> ListarTurmasESeusAlunos([FromRoute] int idTurma, [FromQuery] PaginationFilter paginacao)
        {
            var result = new Result<IPagedList<AlunoTurmaDto>>();

            Validations.IsTrue(idTurma <= 0, result, "Turma", "Id inválido.");

            if (!result.IsValid) return GetIActionResult(result);

            var filtros = new AlunosPorTumaFilter { Id = idTurma };

            if (paginacao == null)
                paginacao = new PaginationFilter();

            paginacao.SortIndex = 0;
            paginacao.SortDirection = SortDirection.Ascending;
            filtros.Pagination = paginacao;

            result = await _alunosPorTumaQuery.Handle(filtros);

            return GetIActionResult(result);
        }
    }
}