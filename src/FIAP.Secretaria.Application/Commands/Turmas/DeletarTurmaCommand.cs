
using FIAP.Secretaria.Shared.Common.Messages;
using FIAP.Secretaria.Shared.Common.Results;
using FluentValidation;
using MediatR;

namespace FIAP.Secretaria.Application.Commands.Turmas;

public class DeletarTurmaCommand : Command, IRequest<Result<bool>>
{
    public int Id { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new Validation().Validate(this);
        return ValidationResult.IsValid;
    }

    internal class Validation : AbstractValidator<DeletarTurmaCommand>
    {
        public Validation()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0)
                .WithErrorCode("Id")
                .WithMessage("O ID da turma é inválido.");
        }
    }
}