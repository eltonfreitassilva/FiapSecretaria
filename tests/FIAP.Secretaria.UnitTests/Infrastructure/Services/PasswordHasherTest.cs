using FIAP.Secretaria.Infrastructure.Services;
using FluentAssertions;
using Xunit;

namespace FIAP.Secretaria.Tests.UnitTests.Infrastructure.Services;

public class PasswordHasherTests
{
    private readonly PasswordHasher _passwordHasher;

    public PasswordHasherTests()
    {
        _passwordHasher = new PasswordHasher();
    }

    [Fact]
    public void Deve_Retornar_Hashes_Diferentes_Para_Mesma_Senha()
    {
        // Arrange
        var senha = "MesmaSenha@123";

        // Act
        var hash1 = _passwordHasher.HashPassword(senha);
        var hash2 = _passwordHasher.HashPassword(senha);

        // Assert
        hash1.Should().NotBe(hash2);
        hash1.Should().NotBeNullOrEmpty();
        hash2.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void Deve_Verificar_Senha_Corretamente_Quando_Senha_Confere()
    {
        // Arrange
        var senha = "senha@123";
        var senhaHash = _passwordHasher.HashPassword(senha);

        // Act
        var resultado = _passwordHasher.VerifyPassword(senha, senhaHash);

        // Assert
        resultado.Should().BeTrue();
    }

    [Fact]
    public void Deve_Rejeitar_Senha_Quando_Senha_Nao_Confere()
    {
        // Arrange
        var senhaCorreta = "SenhaCorreta@123";
        var senhaErrada = "SenhaErrada@123";
        var senhaHash = _passwordHasher.HashPassword(senhaCorreta);

        // Act
        var resultado = _passwordHasher.VerifyPassword(senhaErrada, senhaHash);

        // Assert
        resultado.Should().BeFalse();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Deve_Rejeitar_Quando_Senha_Nula_Ou_Vazia(string senhaInvalida)
    {
        // Arrange
        var senhaHash = _passwordHasher.HashPassword("SenhaValida@123");

        // Act
        var resultado = _passwordHasher.VerifyPassword(senhaInvalida, senhaHash);

        // Assert
        resultado.Should().BeFalse();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    [InlineData("hash-invalido")]
    [InlineData("curto")]
    public void Deve_Rejeitar_Quando_Hash_Invalido(string hashInvalido)
    {
        // Arrange
        var senha = "SenhaQualquer@123";

        // Act
        var resultado = _passwordHasher.VerifyPassword(senha, hashInvalido);

        // Assert
        resultado.Should().BeFalse();
    }

    [Fact]
    public void Deve_Rejeitar_Quando_Hash_De_Outra_Senha()
    {
        // Arrange
        var senha1 = "senha@123";
        var senha2 = "senha2@123";
        var hashSenha1 = _passwordHasher.HashPassword(senha1);

        // Act
        var resultado = _passwordHasher.VerifyPassword(senha2, hashSenha1);

        // Assert
        resultado.Should().BeFalse();
    }

    [Fact]
    public void Deve_Rejeitar_Senha_Errada_Com_Hash_Previamente_Gerado()
    {
        // Arrange 
        var hashPreviamenteGerado = "E2ul9AWOlsTrsYoOt4dzdA==.ZUsd71YlM08qeISPKAo0HVTF8lwu5cTFyh19J1n0a0s=";
        var senhaErrada = "WrongPassword@123";

        // Act
        var resultado = _passwordHasher.VerifyPassword(senhaErrada, hashPreviamenteGerado);

        // Assert
        resultado.Should().BeFalse();
    }
}