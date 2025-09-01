using FIAP.Secretaria.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Secretaria.Infrastructure.Data.EntityConfigurations;

public class MatriculaConfiguration : IEntityTypeConfiguration<Matricula>
{
    public void Configure(EntityTypeBuilder<Matricula> builder)
    {
        builder.ToTable("Matriculas");

        builder.HasKey(m => m.Id);

        builder.Property(c => c.Id)
            .HasColumnName("Id")
            .IsRequired();

        builder.Property(m => m.AlunoId)
            .HasColumnName("AlunoId")
            .IsRequired();

        builder.Property(m => m.TurmaId)
            .HasColumnName("TurmaId")
            .IsRequired();

        builder.Property(m => m.DataMatricula)
            .HasColumnName("DataMatricula")
            .IsRequired();

        builder.HasIndex(m => new { m.AlunoId, m.TurmaId })
            .IsUnique();

        builder.HasOne(m => m.Aluno)
            .WithMany(a => a.Matriculas)
            .HasForeignKey(m => m.AlunoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.Turma)
            .WithMany(t => t.Matriculas)
            .HasForeignKey(m => m.TurmaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}