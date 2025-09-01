using FluentValidation;
using MediatR;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Common.Messages;

public class CadastrarTurmaCommand : Command, IRequest<Result<bool>>
{
    public string Nome { get; set; }
    public string Descricao { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new Validation().Validate(this);
        return ValidationResult.IsValid;
    }

    internal class Validation : AbstractValidator<CadastrarTurmaCommand>
    {
        public Validation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithErrorCode("Nome")
                .WithMessage("O nome da turma é obrigatório.")
                .MinimumLength(3)
                .WithErrorCode("Nome")
                .WithMessage("O nome da turma deve ter pelo menos 3 caracteres.")
                .MaximumLength(100)
                .WithErrorCode("Nome")
                .WithMessage("O nome da turma não pode ter mais de 100 caracteres.");

            RuleFor(c => c.Descricao)
                .NotEmpty()
                .WithErrorCode("Descricao")
                .WithMessage("A descrição da turma é obrigatória.")
                .MinimumLength(10)
                .WithErrorCode("Descricao")
                .WithMessage("A descrição deve ter pelo menos 10 caracteres.")
                .MaximumLength(255)
                .WithErrorCode("Descricao")
                .WithMessage("A descrição não pode ter mais de 255 caracteres.");
        }
    }
}