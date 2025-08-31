using FIAP.Secretaria.Domain.Entities;
using FIAP.Secretaria.Shared.Common.Data;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Secretaria.Infrastructure.Data;

public class SecretariaContext : DbContext, IUnitOfWork
{
    public SecretariaContext(DbContextOptions<SecretariaContext> options) : base(options) { }

    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Turma> Turmas { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    public async Task<bool> CommitAsync() => await SaveChangesAsync() > 0;

    public async Task RollbackAsync() => await Task.CompletedTask;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SecretariaContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}