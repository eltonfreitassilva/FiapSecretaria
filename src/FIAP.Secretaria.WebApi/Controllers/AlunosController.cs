using FIAP.Secretaria.Application.Commands.Alunos;
using FIAP.Secretaria.Application.Dtos;
using FIAP.Secretaria.Application.Filters.Alunos;
using FIAP.Secretaria.Application.Queries.Alunos;
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
    public class AlunosController : DefaultController
    {
        private readonly IConsultarAlunosQuery _consultarAlunosQuery;
        public AlunosController(IMediator mediator, IConsultarAlunosQuery consultarAlunosQuery) : base(mediator)
        {
            _consultarAlunosQuery = consultarAlunosQuery;
        }

        [HttpPost]
        [SwaggerOperation(summary: "Efetuar cadastro de um aluno.", description: "Permite realizar o cadastro de um aluno.")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Result<bool>))]
        public async Task<IActionResult> Autenticar([FromBody] CadastrarAlunoCommand command)
        {
            var result = await _mediator.Send(command);

            return GetIActionResult(result);
        }

        [HttpPut("{id:int}")]
        [SwaggerOperation(summary: "Editar o cadastro do aluno.", description: "Permite editar o cadastro de um aluno.")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Result<bool>))]
        public async Task<IActionResult> EditarAluno(int id, [FromBody] EditarAlunoCommand command)
        {
            command.Id = id;

            var result = await _mediator.Send(command);

            return GetIActionResult(result);
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(summary: "Deletar o cadastro do aluno.", description: "Permite deletar o cadastro de um aluno.")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Result<bool>))]
        public async Task<IActionResult> DeletarAluno([FromRoute] int id, [FromBody] DeletarAlunoCommand command)
        {
            command = new DeletarAlunoCommand { Id = id };

            var result = await _mediator.Send(command);

            return GetIActionResult(result);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(summary: "Obter aluno por id", description: "Permite obter um aluno por id.")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Result<IPagedList<AlunoDto>>))]
        public async Task<IActionResult> ObterAlunoPorId([FromRoute] int id, [FromQuery] PaginationFilter paginacao)
        {
            var result = new Result<IPagedList<AlunoDto>>();

            Validations.IsTrue(id <= 0, result, "Aluno", "Id inválido.");

            if (result.IsValid)
            {
               var filtros = new ConsultarAlunosFilter { Id = id };

                if (paginacao == null)
                    paginacao = new();

                paginacao.SortIndex = 0;
                paginacao.SortDirection = SortDirection.Ascending;
                filtros.Pagination = paginacao;

                result = await _consultarAlunosQuery.Handle(filtros);
            }

            return GetIActionResult(result);
        }

        [HttpGet]
        [SwaggerOperation(summary: "Listar alunos", description: "Permite listar alunos.")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Result<IPagedList<AlunoDto>>))]
        public async Task<IActionResult> ListarAlunos([FromQuery] ConsultarAlunosFilter filtros, [FromQuery] PaginationFilter paginacao)
        {
                if (filtros == null)
                    filtros = new();

                if (paginacao == null)
                    paginacao = new();

                paginacao.SortIndex = 0;
                paginacao.SortDirection = SortDirection.Ascending;
                filtros.Pagination = paginacao;

               var result = await _consultarAlunosQuery.Handle(filtros);

            return GetIActionResult(result);
        }

        [HttpGet("{nome}")]
        [SwaggerOperation(summary: "Obter aluno pelo nome", description: "Permite obter um aluno pelo nome.")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Result<IPagedList<AlunoDto>>))]
        public async Task<IActionResult> ObterAlunoNome([FromRoute] string nome, [FromQuery] PaginationFilter paginacao)
        {
            var result = new Result<IPagedList<AlunoDto>>();

            Validations.IsTrue(string.IsNullOrWhiteSpace(nome) || nome.Length < 3, result, "Aluno", "O nome para busca deve ter no mínimo 3 caracteres.");

            if (result.IsValid)
            {
                var filtros = new ConsultarAlunosFilter { Nome = nome };

                if (paginacao == null)
                    paginacao = new();

                paginacao.SortIndex = 0;
                paginacao.SortDirection = SortDirection.Ascending;
                filtros.Pagination = paginacao;

                result = await _consultarAlunosQuery.Handle(filtros);
            }

            return GetIActionResult(result);
        }
    }
}