using FluentValidation;
using MediatR;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Common.Messages;

public class CadastrarMatriculaCommand : Command, IRequest<Result<bool>>
{
    public int AlunoId { get; set; }
    public int TurmaId { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new Validation().Validate(this);
        return ValidationResult.IsValid;
    }

    internal class Validation : AbstractValidator<CadastrarMatriculaCommand>
    {
        public Validation()
        {
            RuleFor(c => c.AlunoId)
                .GreaterThan(0)
                .WithErrorCode("AlunoId")
                .WithMessage("O ID do aluno é obrigatório.");

            RuleFor(c => c.TurmaId)
                .GreaterThan(0)
                .WithErrorCode("TurmaId")
                .WithMessage("O ID da turma é obrigatório.");
        }
    }
}