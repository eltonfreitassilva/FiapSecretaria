namespace FIAP.Secretaria.Shared.Common.Results;

/// <summary>
/// Enumerador para representar status do result. Segue a numeração do Http para facilitar conversões.
/// </summary>
public enum ResultCode
{
    /// <summary>Indica que um resultado foi bem sucedido.</summary>
    Ok = 200,

    /// <summary>Indica que ocorreu um erro genérico no resultado.</summary>
    GenericError = 500,

    /// <summary>Indica que ocorreu uma validação de negócio no resultado.</summary>
    BusinessError = 422,

    /// <summary>Indica que os parâmetros informados estão incorretos.</summary>
    BadRequest = 400,

    /// <summary>Indica que o servidor não autorizou a emissão de um resposta ao autor da requisição.</summary>
    Forbidden = 403,
}
