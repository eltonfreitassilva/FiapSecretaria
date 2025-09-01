using FIAP.Secretaria.Shared.Common.Results;

namespace FIAP.Secretaria.Shared.Common.Validators;

public static class Validations
{
    public static void IsNull<T>(T entity, Result result, string attribute, string message)
    {
        if (entity == null)
            result.AddValidation(attribute, message);
    }

    public static void IsNotNull<T>(T entity, Result result, string attribute, string message)
    {
        if (entity != null)
            result.AddValidation(attribute, message);
    }

    public static void IsNullOrWhiteSpace(string value, Result result, string attribute, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
            result.AddValidation(attribute, message);
    }

    public static void IsNullOrEmpty(string value, Result result, string attribute, string message)
    {
        if (string.IsNullOrEmpty(value))
            result.AddValidation(attribute, message);
    }

    public static void IsTrue(bool value, Result result, string attribute, string message)
    {
        if (value)
            result.AddValidation(attribute, message);
    }

    public static void IsFalse(bool value, Result result, string attribute, string message)
    {
        if (!value)
            result.AddValidation(attribute, message);
    }
}