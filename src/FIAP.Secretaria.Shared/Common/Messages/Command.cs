using System.Text.Json.Serialization;
using FIAP.Secretaria.Shared.Common.Results;
using FIAP.Secretaria.Shared.Utils.Extensions;
using FluentValidation.Results;

namespace FIAP.Secretaria.Shared.Common.Messages;

public abstract class Command : Message
{
    [JsonIgnore]
    public DateTime Timestamp { get; protected set; }
    [JsonIgnore]
    public ValidationResult ValidationResult { get; protected set; } = new();

    protected Command()
    {
        Timestamp = DateTime.Now;
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
