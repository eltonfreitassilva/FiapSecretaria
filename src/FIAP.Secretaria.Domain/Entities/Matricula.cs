using FIAP.Secretaria.Shared.Common.Entities;

namespace FIAP.Secretaria.Domain.Entities;

public class Matricula : Entity
{
    protected Matricula() { }

    public int AlunoId { get; private set; }
    public int TurmaId { get; private set; }
    public DateTime DataMatricula { get; private set; }
    
    public virtual Aluno Aluno { get; private set; }
    public virtual Turma Turma { get; private set; }

   public static class Factory
    {
        public static Matricula Criar(int alunoId, int turmaId)
        {
            return new Matricula
            {
                AlunoId = alunoId,
                TurmaId = turmaId,
                DataMatricula = DateTime.Now
            };
        }
    }
}
