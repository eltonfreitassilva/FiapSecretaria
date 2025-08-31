using FIAP.Secretaria.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Secretaria.Infrastructure.Data.EntityConfigurations;

public class TurmaConfiguration : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> builder)
    {
        builder.ToTable("Turmas");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Descricao)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasMany(t => t.Matriculas)
            .WithOne(m => m.Turma)
            .HasForeignKey(m => m.TurmaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
