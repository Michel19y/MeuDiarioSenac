using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
namespace MeuDiarioSenac.Data;

public class MeuDiarioSenacContext : DbContext
{
    private static string connectionString = "Server=localhost;Database=sistema_registros;Uid=root;Pwd=1234;";


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
   
}