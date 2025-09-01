namespace FIAP.Secretaria.Application.Dtos;

public class MatriculaDto
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public int TurmaId { get; set; }
    public DateTime DataMatricula { get; set; }
    public string AlunoNome { get; set; }
    public string TurmaNome { get; set; }
}