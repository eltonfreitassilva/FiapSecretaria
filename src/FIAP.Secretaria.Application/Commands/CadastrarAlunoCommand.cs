using FluentValidation;
using FIAP.Secretaria.Shared.Utils.Helpers;
using MediatR;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Common.Messages;

public class CadastrarAlunoCommand : Command, IRequest<Result<bool>>
{
    public string Nome { get; set; }
    public string Senha { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new Validation().Validate(this);
        return ValidationResult.IsValid;
    }

    internal class Validation : AbstractValidator<CadastrarAlunoCommand>
    {
        public Validation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithErrorCode("Nome")
                .WithMessage("O nome é obrigatório.")
                .MinimumLength(3)
                .WithErrorCode("Nome")
                .WithMessage("O nome deve ter pelo menos 3 caracteres.")
                .MaximumLength(100)
                .WithErrorCode("Nome")
                .WithMessage("O nome não pode ter mais de 100 caracteres.")
                .Matches(@"^[a-zA-ZÀ-ÿ\s]+$")
                .WithErrorCode("Nome")
                .WithMessage("O nome deve conter apenas letras e espaços.");

            RuleFor(c => c.Email)
                .NotEmpty()
                .WithErrorCode("E-mail")
                .WithMessage("O e-mail é obrigatório.")
                .EmailAddress()
                .WithErrorCode("E-mail")
                .WithMessage("O e-mail informado não é válido.")
                .MaximumLength(100)
                .WithErrorCode("E-mail")
                .WithMessage("O e-mail não pode ter mais de 100 caracteres.");

            RuleFor(c => c.Senha)
                .NotEmpty()
                .WithErrorCode("Senha")
                .WithMessage("A senha é obrigatória.")
                .Must(SenhaHelper.ValidarSenha)
                .WithErrorCode("Senha")
                .WithMessage("A senha deve ter no mínimo 8 caracteres, contendo letras maiúsculas, minúsculas, números e símbolos especiais.");

            RuleFor(c => c.Cpf)
                .NotEmpty()
                .WithErrorCode("CPF")
                .WithMessage("O CPF é obrigatório.")
                .Must(cpf => CpfHelper.IsCpfValid(cpf))
                .WithErrorCode("CPF")
                .WithMessage("O CPF informado não é válido.");

            RuleFor(c => c.DataNascimento)
                .NotEmpty()
                .WithErrorCode("DataNascimento")
                .WithMessage("A data de nascimento é obrigatória.")
                .LessThan(DateTime.Now.AddYears(-5))
                .WithErrorCode("DataNascimento")
                .WithMessage("A data de nascimento deve ser válida.")
                .GreaterThan(DateTime.Now.AddYears(-100))
                .WithErrorCode("DataNascimento")
                .WithMessage("A data de nascimento deve ser válida.");
        }
    }
}