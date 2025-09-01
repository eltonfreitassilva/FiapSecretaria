using FIAP.Secretaria.Shared.Common.Data;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Utils.Extensions;
using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace FIAP.Secretaria.Shared.Common.Queries;

public abstract class Filter
{
	[JsonIgnore] public DateTime StartTimestamp { get; protected set; }
	[JsonIgnore] public DateTime? EndTimestamp { get; protected set; }
	[JsonIgnore] public ValidationResult ValidationResult { get; protected set; } = new();
	[JsonIgnore] public PaginationFilter Pagination { get; set; }

	public int TimeExecutionInMilliseconds
	{
		get
		{
			if (!EndTimestamp.HasValue) return 0;

			return EndTimestamp.Value.Subtract(StartTimestamp).Milliseconds;
		}
	}

	protected Filter()
	{
		StartTimestamp = DateTime.Now;
	}

	public abstract bool IsValid();

	public void Validate(Result result)
	{
		IsValid();

		if (!ValidationResult.Errors.IsNullOrEmpty())
			ValidationResult.Errors.ForEach(error =>
			{
				result.AddValidation(error.ErrorCode, error.ErrorMessage);
			});
	}
}
