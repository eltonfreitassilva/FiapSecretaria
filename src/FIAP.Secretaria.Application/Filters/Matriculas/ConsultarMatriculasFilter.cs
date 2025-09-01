using FIAP.Secretaria.Application.Dtos;
using FIAP.Secretaria.Shared.Common.Queries;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Utils.Pagination;
using FluentValidation;

namespace FIAP.Secretaria.Application.Filters.Matriculas;

public class ConsultarMatriculasFilter : Filter, IQueryFilter<Result<IPagedList<MatriculaDto>>>
{
    public int? Id { get; set; } = null;
    public int? AlunoId { get; set; } = null;
    public int? TurmaId { get; set; } = null;
    public DateTime? DataMatriculaInicio { get; set; } = null;
    public DateTime? DataMatriculaFim { get; set; } = null;

    public override bool IsValid()
    {
        ValidationResult = new Validation().Validate(this);
        return ValidationResult.IsValid;
    }

    internal class Validation : AbstractValidator<ConsultarMatriculasFilter>
    {
        public Validation() { }
    }
}