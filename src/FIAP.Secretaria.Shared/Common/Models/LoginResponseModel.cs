﻿namespace FIAP.Secretaria.Shared.Common.Models;

public class LoginResponseModel
{
    public string Token { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Perfil { get; set; } = string.Empty;
}