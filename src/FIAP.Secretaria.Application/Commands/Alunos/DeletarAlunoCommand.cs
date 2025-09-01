using FIAP.Secretaria.Shared.Common.Messages;
using FIAP.Secretaria.Shared.Common.Results;
using FluentValidation;
using MediatR;

public class DeletarAlunoCommand : Command, IRequest<Result<bool>>
{
    public int Id { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new Validation().Validate(this);
        return ValidationResult.IsValid;
    }

    internal class Validation : AbstractValidator<DeletarAlunoCommand>
    {
        public Validation()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0)
                .WithMessage("ID do aluno é obrigatório.");
        }
    }
}