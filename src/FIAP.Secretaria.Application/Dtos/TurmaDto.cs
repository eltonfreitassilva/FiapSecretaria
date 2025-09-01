namespace FIAP.Secretaria.Application.Dtos;

public class TurmaDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
    public DateTime DataAlteracao { get; set; }
    public int QuantidadeAlunos { get; set; }
}