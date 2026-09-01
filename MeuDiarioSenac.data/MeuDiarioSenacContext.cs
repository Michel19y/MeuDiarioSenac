using Microsoft.EntityFrameworkCore;
using MeuDiarioSenac.Models;

namespace MeuDiarioSenac.Data;

public class MeuDiarioSenacContext : DbContext
{
    private static string connectionString = "Server=localhost;Database=sistema_registros;Uid=root;Pwd=1234;";

    // Mapeamento da tabela de usuários para o Entity Framework
    public DbSet<Usuario> Usuarios { get; set; }

    // Mapeamento da tabela de registros para o Entity Framework
    public DbSet<Registro> Registros { get; set; }

    // Configuração do provider MySQL
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    // A tabela "registros" já existe no banco (criada manualmente, snake_case) -
    // mapeada aqui em vez de deixar o EF assumir a convenção "Registros"/PascalCase.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Registro>(entity =>
        {
            entity.ToTable("registros");
            entity.Property(r => r.UsuarioId).HasColumnName("usuario_id");
            entity.Property(r => r.Titulo).HasColumnName("titulo");
            entity.Property(r => r.Data).HasColumnName("data");
            entity.Property(r => r.Conteudo).HasColumnName("conteudo");
        });
    }
}