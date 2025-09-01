using FIAP.Secretaria.Application.Commands.Alunos;
using FIAP.Secretaria.Shared.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace FIAP.Secretaria.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AlunosController : DefaultController
    {
        public AlunosController(IMediator mediator) : base(mediator)
        {
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
        public async Task<IActionResult> DeletarAluno(int id, [FromBody] DeletarAlunoCommand command)
        {
            command.Id = id;

            var result = await _mediator.Send(command);

            return GetIActionResult(result);
        }
    }
}