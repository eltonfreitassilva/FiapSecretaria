using FIAP.Secretaria.Application.Dtos;
using FIAP.Secretaria.Shared.Common.Queries;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Utils.Pagination;
using FluentValidation;

namespace FIAP.Secretaria.Application.Filters.Alunos;

public class ConsultarAlunosFilter : Filter, IQueryFilter<Result<IPagedList<AlunoDto>>>
{
    public int? Id { get; set; } = null;

    public override bool IsValid()
    {
        ValidationResult = new Validation().Validate(this);

        return ValidationResult.IsValid;
    }

    internal class Validation : AbstractValidator<ConsultarAlunosFilter>
    {
        public Validation() { }
    }
}
