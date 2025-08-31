using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace FIAP.Secretaria.Shared.Common.Results;

/// <summary>
/// Representa um resultado de operação. Por padrão o resultado é assumido como válido ao ser instanciado.
/// </summary>
[DataContract]
public class Result
{
    private const string UNEXPECTED_ERROR_MESSAGE = "Unexpected Error";
    private const string VALIDATIONS_MESSAGE = "Validations";
    private const string SUCCESS_MESSAGE = "Success";

    private List<ResultValidation> _validations = new List<ResultValidation>();
    private readonly object _lockObject = new object();

    /// <summary>
    /// Construtor padrão.
    /// </summary>
    public Result()
    {
        SetToOK();
    }

    /// <summary>
    /// Erros de validações ocorridas. Geralmente usadas para validações sobre propriedades.
    /// </summary>
    [DataMember(Name = "validations")]
    public IReadOnlyList<ResultValidation> Validations => _validations.AsReadOnly();

    /// <summary>
    /// Mensagem do resultado.
    /// </summary>
    [DataMember(Name = "message")]
    public string Message { get; private set; }

    /// <summary>
    /// Protocolo para tracking.
    /// </summary>
    [DataMember(Name = "protocol")]
    public Guid Protocol { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Código do resultado. Utilize a propriedade Valid para checar de forma fácil se o resultado foi bem sucedido ou não.
    /// </summary>
    [IgnoreDataMember]
    public ResultCode ResultCode { get; private set; }

    /// <summary>
    /// Indica se o resultado foi bem sucedido ou não.
    /// </summary>
    [JsonIgnore]
    public bool IsValid => (int)ResultCode < 400;

    /// <summary>
    /// Sucesso do resultado.
    /// </summary>
    [DataMember(Name = "success")]
    public bool Success => ResultCode == ResultCode.Ok;

    /// <summary>
    /// Código numérico do resultado para serialização.
    /// </summary>
    [DataMember(Name = "code")]
    public int Code => (int)ResultCode;

    /// <summary>
    /// Indica se o resultado é válido para serialização.
    /// </summary>
    [DataMember(Name = "isValid")]
    public bool IsValidSerialized => IsValid;

    /// <summary>
    /// Configura este result com as informações desejadas.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <param name="validations"></param>
    public virtual Result Set(ResultCode code, string message, List<ResultValidation> validations)
    {
        Set(code, message);
        _validations = validations ?? new List<ResultValidation>();

        return this;
    }

    /// <summary>
    /// Configura este result com as informações desejadas.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    public virtual Result Set(ResultCode code, string message)
    {
        switch (code)
        {
            case ResultCode.Ok:
                SetToOK(message);
                break;

            case ResultCode.GenericError:
                Message = string.IsNullOrWhiteSpace(message) ? UNEXPECTED_ERROR_MESSAGE : message;
                ResultCode = ResultCode.GenericError;
                break;

            case ResultCode.BusinessError:
                Message = message;
                ResultCode = ResultCode.BusinessError;
                break;

            case ResultCode.Forbidden:
                Message = message;
                ResultCode = ResultCode.Forbidden;
                break;

            default:
                Message = message;
                ResultCode = code;
                break;
        }

        return this;
    }

    /// <summary>
    /// Configura este result para o status "BusinessError" (ou seja, inválido) com a mensagem desejada.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public virtual Result SetBusinessMessage(string message)
    {
        Message = message;
        ResultCode = ResultCode.BusinessError;

        return this;
    }

    /// <summary>
    /// Configura este result com as informações de outro result (message, messageCode e validations).
    /// </summary>
    /// <param name="result"></param>
    public virtual Result SetFromAnother(Result result)
    {
        if (result == null)
            return this;

        Message = result.Message;
        ResultCode = result.ResultCode;
        Protocol = result.Protocol;

        _validations.Clear();
        if (result.Validations != null && result.Validations.Any())
        {
            _validations.AddRange(result.Validations);
        }

        return this;
    }

    /// <summary>
    /// Adiciona uma validação e seta o result code para BusinessError caso o status atual seja OK (válido).
    /// </summary>
    /// <param name="attribute"></param>
    /// <param name="message"></param>
    public virtual Result AddValidation(string attribute, string message)
    {
        if (string.IsNullOrWhiteSpace(attribute))
            throw new ArgumentException($"The parameter {nameof(attribute)} is required to add validation to result.");

        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException($"The parameter {nameof(message)} is required to add validation to result.");

        _validations.Add(new ResultValidation(attribute, message));

        if (ResultCode == ResultCode.Ok)
            Set(ResultCode.BusinessError, VALIDATIONS_MESSAGE);

        return this;
    }

    /// <summary>
    /// Adiciona uma validação de forma thread-safe.
    /// </summary>
    /// <param name="attribute"></param>
    /// <param name="message"></param>
    public virtual Result AddValidationThreadSafe(string attribute, string message)
    {
        lock (_lockObject)
        {
            return AddValidation(attribute, message);
        }
    }

    /// <summary>
    /// Adiciona validações condicionalmente.
    /// </summary>
    /// <param name="isNotValid"></param>
    /// <param name="attribute"></param>
    /// <param name="message"></param>
    public virtual Result AddValidationsIfFails(bool isNotValid, string attribute, string message)
    {
        if (isNotValid)
            return AddValidation(attribute, message);

        return this;
    }

    /// <summary>
    /// Limpa todas as validações do resultado.
    /// </summary>
    public virtual Result ClearValidations()
    {
        _validations.Clear();
        return this;
    }

    /// <summary>
    /// Configura o protocolo do resultado.
    /// </summary>
    /// <param name="protocol"></param>
    public virtual Result WithProtocol(Guid protocol)
    {
        Protocol = protocol;
        return this;
    }

    /// <summary>
    /// Retorna a mensagem e possíveis valicações do result.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        if (IsValid && _validations.Count == 0)
            return Message;

        var sb = new StringBuilder();
        sb.Append(Message);

        if (!IsValid && _validations != null && _validations.Any())
        {
            sb.AppendLine("");
            _validations.ForEach(val => sb.AppendLine($"- {val.Attribute}: {val.Message}"));
        }

        string toString = sb.ToString();

        if (toString.EndsWith("\r\n"))
            toString = toString.Substring(0, toString.Length - 2);

        return toString;
    }

    /// <summary>
    /// Configura este result para status OK (válido).
    /// </summary>
    private void SetToOK(string msg = null)
    {
        Message = string.IsNullOrWhiteSpace(msg) ? SUCCESS_MESSAGE : msg;
        ResultCode = ResultCode.Ok;
    }
}

/// <summary>
/// Representa um resultado de operação, com retorno genérico. Por padrão o resultado é assumido como válido ao ser instanciado.
/// </summary>
public class Result<T> : Result
{
    /// <summary>
    /// Construtor padrão.
    /// </summary>
    public Result() : base() { }

    /// <summary>
    /// Valor de retorno do result. Só deve possuir valor caso seja válido.
    /// </summary>
    [DataMember(Name = "data")]
    public T Data { get; set; }

    /// <summary>
    /// Adiciona uma validação e seta o result code para BusinessError caso o status atual seja OK (válido).
    /// </summary>
    /// <param name="attribute"></param>
    /// <param name="message"></param>
    public new Result<T> AddValidation(string attribute, string message)
    {
        base.AddValidation(attribute, message);
        return this;
    }

    /// <summary>
    /// Configura este result com as informações desejadas.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    public new Result<T> Set(ResultCode code, string message)
    {
        base.Set(code, message);
        return this;
    }

    /// <summary>
    /// Configura este result com as informações desejadas.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <param name="validations"></param>
    public new Result<T> Set(ResultCode code, string message, List<ResultValidation> validations)
    {
        base.Set(code, message, validations);
        return this;
    }

    /// <summary>
    /// Configura este result para o status "BusinessError" (ou seja, inválido) com a mensagem desejada.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public new Result<T> SetBusinessMessage(string message)
    {
        base.SetBusinessMessage(message);
        return this;
    }

    /// <summary>
    /// Configura este result com as informações de outro result (message, messageCode e validations).
    /// </summary>
    /// <param name="result"></param>
    public Result<T> SetFromAnother(Result<T> result)
    {
        base.SetFromAnother(result);
        Data = result.Data;
        return this;
    }
}