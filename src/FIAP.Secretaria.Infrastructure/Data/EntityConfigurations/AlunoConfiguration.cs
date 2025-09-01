using FIAP.Secretaria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Secretaria.Infrastructure.Data.EntityConfigurations;

public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.ToTable("Alunos");

        builder.HasKey(a => a.Id);

        builder.Property(c => c.Id)
            .HasColumnName("Id")
            .IsRequired();

        builder.Property(a => a.Nome)
            .HasColumnName("Nome")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Cpf)
            .HasColumnName("Cpf")
            .IsRequired()
            .HasMaxLength(11)
            .IsFixedLength();

        builder.Property(a => a.Email)
            .HasColumnName("Email")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Senha)
            .HasColumnName("Senha")
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(m => m.DataCriacao)
            .HasColumnName("DataCriacao")
            .IsRequired();

        builder.Property(m => m.DataAlteracao)
        .HasColumnName("DataAlteracao");

        builder.Property(m => m.Ativo)
            .HasColumnName("Ativo")
            .IsRequired();

        builder.HasIndex(a => a.Cpf)
            .IsUnique();

        builder.HasIndex(a => a.Email)
            .IsUnique();

        builder.HasMany(a => a.Matriculas)
            .WithOne(m => m.Aluno)
            .HasForeignKey(m => m.AlunoId);
    }
}