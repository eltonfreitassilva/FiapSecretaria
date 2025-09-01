namespace FIAP.Secretaria.Application.Dtos;

public class AlunoTurmaDto
{
    public int AlunoId { get; set; }
    public string AlunoNome { get; set; } = string.Empty;
    public string AlunoEmail { get; set; } = string.Empty;
    public DateTime DataMatricula { get; set; }
}