namespace FIAP.Secretaria.Shared.Common.Results;

/// <summary>
/// Classe para representar uma validação do result.
/// </summary>
public class ResultValidation
{
    public ResultValidation(string attribute, string message)
    {
        Attribute = attribute;
        Message = message;
    }

    /// <summary>
    /// Campo da validação. Ex: "Cep".
    /// </summary>
    public string Attribute { get; protected set; }

    /// <summary>
    /// Mensagem para a validação do campo. Ex: "Campo obrigatório".
    /// </summary>
    public string Message { get; protected set; }

    public static bool operator ==(ResultValidation a, ResultValidation b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(ResultValidation a, ResultValidation b) => !(a == b);

    public override bool Equals(object obj)
    {
        var compareTo = obj as ResultValidation;

        if (ReferenceEquals(this, compareTo)) return true;
        if (ReferenceEquals(null, compareTo)) return false;

        return Attribute.Equals(compareTo.Attribute) &&
            Message.Equals(compareTo.Message);
    }

    public override int GetHashCode()
    {
        return (GetType().GetHashCode() * 907) + Attribute.GetHashCode() +
            (GetType().GetHashCode() * 907) + Message.GetHashCode();
    }

    public override string ToString() => $"{Attribute} - {Message}";
}
