using FIAP.Secretaria.Shared.Common.Messages;
using FIAP.Secretaria.Shared.Common.Models;
using FIAP.Secretaria.Shared.Common.Results;
using FluentValidation;
using MediatR;

namespace FIAP.Secretaria.Application.Commands.Acesso;

public class LoginCommand : Command, IRequest<Result<LoginResponseModel>>
{
    public string Email { get; set; }
    public string Senha { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new Validation().Validate(this);

        return ValidationResult.IsValid;
    }

    internal class Validation : AbstractValidator<LoginCommand>
    {
        public Validation()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .WithErrorCode("E-mail")
                .WithMessage("O e-mail é obrigatório.")
                .EmailAddress()
                .WithErrorCode("E-mail")
                .WithMessage("O e-mail informado não é válido.");

            RuleFor(c => c.Senha)
                .NotEmpty()
                .WithErrorCode("Senha")
                .WithMessage("A senha é obrigatória.")
                .MinimumLength(8)
                .WithErrorCode("Senha")
                .WithMessage("A senha deve conter no mínimo 8 caracteres.");
        }
    }
}