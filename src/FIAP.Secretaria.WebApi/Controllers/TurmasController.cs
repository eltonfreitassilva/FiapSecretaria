using FIAP.Secretaria.Application.Commands.Turmas;
using FIAP.Secretaria.Application.Dtos;
using FIAP.Secretaria.Application.Filters;
using FIAP.Secretaria.Application.Filters.Turmas;
using FIAP.Secretaria.Application.Queries;
using FIAP.Secretaria.Application.Queries.Turmas;
using FIAP.Secretaria.Shared.Common.Data;
using FIAP.Secretaria.Shared.Common.Enums;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Common.Validators;
using FIAP.Secretaria.Shared.Utils.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace FIAP.Secretaria.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TurmasController : DefaultController
    {
         private readonly IConsultarTurmasQuery _consultarTurmasQuery;

        public TurmasController(IMediator mediator, IConsultarTurmasQuery consultarTurmasQuery) : base(mediator)
        {
            _consultarTurmasQuery = consultarTurmasQuery;
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
    }
}