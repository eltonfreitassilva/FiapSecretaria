using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Common.Models;
using MediatR;
using FIAP.Secretaria.Domain.Interfaces.Repositories;
using FIAP.Secretaria.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using FIAP.Secretaria.Shared.Common.Validators;
using FIAP.Secretaria.Application.Commands.Acesso;

namespace FIAP.Secretaria.Application.CommandsHandlers.Acesso;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponseModel>>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;
    public LoginCommandHandler(
        IUsuarioRepository usuarioRepository,
        ITokenService tokenService,
        IPasswordHasher passwordHasher)
    {
        _usuarioRepository = usuarioRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
    }
    public async Task<Result<LoginResponseModel>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var result = new Result<LoginResponseModel>();
        command.Validate(result);

        if (result.IsValid)
        {
            var usuario = await _usuarioRepository.AsQueryable()
                .AsNoTracking()
                .Where(u => u.Email.Equals(command.Email))
                .FirstOrDefaultAsync(cancellationToken);

            Validations.IsNull(usuario, result, "Login", "Usuário ou senha inválidos.");

            if (result.IsValid)
            {
                var senhaValida = _passwordHasher.VerifyPassword(command.Senha, usuario.Senha);

                Validations.IsFalse(senhaValida, result, "Login", "Usuário ou senha inválidos.");

                if (result.IsValid)
                {
                    var token = _tokenService.GenerateToken(usuario.Id, usuario.Email, usuario.Perfil.ToString());

                    var response = new LoginResponseModel
                    {
                        Nome = usuario.Nome,
                        Email = usuario.Email,
                        Token = token,
                        Perfil = usuario.Perfil.ToString()
                    };

                    result.Data = response;
                }
            }
        }

        return result;
    }
}
