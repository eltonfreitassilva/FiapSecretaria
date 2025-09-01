namespace FIAP.Secretaria.Shared.Utils.Helpers;

public static class SenhaHelper
{
    public static bool ValidarSenha(string senha)
    {
        if (string.IsNullOrWhiteSpace(senha) || senha.Length < 8)
            return false;

        bool temMaiuscula = senha.Any(c => char.IsUpper(c));
        bool temMinuscula = senha.Any(c => char.IsLower(c));
        bool temNumero = senha.Any(c => char.IsDigit(c));
        bool temSimbolo = senha.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c));

        return temMaiuscula && temMinuscula && temNumero && temSimbolo;
    }
}
