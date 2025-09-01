using FIAP.Secretaria.Application.Dtos;
using FIAP.Secretaria.Shared.Common.Queries;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Utils.Pagination;
using FluentValidation;

namespace FIAP.Secretaria.Application.Filters.Turmas;

public class AlunosPorTumaFilter : Filter, IQueryFilter<Result<IPagedList<AlunoTurmaDto>>>
{
    public int? Id { get; set; } = null;
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool? Ativas { get; set; } = true;

    public override bool IsValid()
    {
        ValidationResult = new Validation().Validate(this);
        return ValidationResult.IsValid;
    }

    internal class Validation : AbstractValidator<AlunosPorTumaFilter>
    {
        public Validation() { }
    }
}

