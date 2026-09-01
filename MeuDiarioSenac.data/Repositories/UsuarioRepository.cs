using System.Linq;
using MeuDiarioSenac.Models;

namespace MeuDiarioSenac.Data.Repositories;

public class UsuarioRepository
{
    public static Usuario? RealizarLogin(string email, string senha)
    {
        using var context = new MeuDiarioSenacContext();
        return context.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
    }

    public static bool CadastrarUsuario(string nome, string email, string senha)
    {
        using var context = new MeuDiarioSenacContext();
        try
        {
            var novoUsuario = new Usuario { Nome = nome, Email = email, Senha = senha };
            context.Usuarios.Add(novoUsuario);
            context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
