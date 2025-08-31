using FIAP.Secretaria.Infrastructure.Security;
using FIAP.Secretaria.Shared.Common.Models;
using FIAP.Secretaria.Shared.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FIAP.Secretaria.WebApi.Controllers;

[Authorize]
public class DefaultController : ControllerBase
{
    protected readonly IMediator _mediator;

    protected DefaultController(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected IUserAccessor UserAccessor => HttpContext.RequestServices.GetService<IUserAccessor>() ?? new EmptyUserAccessor();
    protected UsuarioAutenticadoModel UsuarioAutenticado
    {
        get => new (UserAccessor.GetUserId().Value, UserAccessor.GetUserName(), UserAccessor.GetUserEmail(), UserAccessor.GetUserRole());
    }

    [NonAction]
    protected IActionResult GetIActionResult(Result result)
    {
        if (result == null)
            return StatusCode((int)HttpStatusCode.InternalServerError, null);

        return StatusCode((int)result.ResultCode, result);
    }
}