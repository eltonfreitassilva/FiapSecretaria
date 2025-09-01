using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace FIAP.Secretaria.WebApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SobreController : DefaultController
    {
        public SobreController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Route("Obter")]
        [SwaggerOperation(summary: "Obter informações da aplicação.", description: "Permite obter as informações gerais e de versionamento da aplicação.")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Result<SobreModel>))]
        public IActionResult Get()
        {
            var result = new Result<SobreModel>();

            result.Data = new SobreModel();

            return GetIActionResult(result);
        }
    }
}