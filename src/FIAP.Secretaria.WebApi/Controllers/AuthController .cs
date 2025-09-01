using FIAP.Secretaria.Application.Commands.Acesso;
using FIAP.Secretaria.Shared.Common.Models;
using FIAP.Secretaria.Shared.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace FIAP.Secretaria.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : DefaultController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        [SwaggerOperation(summary: "Efetuar autenticação de um usuário.", description: "Permite realizar a autenticação de um usuário e obter o respectivo bearer token.")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Result<LoginResponseModel>))]
        public async Task<IActionResult> Autenticar([FromBody] LoginCommand login)
        {
            var result = await _mediator.Send(login);

            return GetIActionResult(result);
        }
    }
}
