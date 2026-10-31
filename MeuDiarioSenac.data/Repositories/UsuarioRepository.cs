using System.Linq;
using MeuDiarioSenac.Model;

namespace MeuDiarioSenac.Data.Repositories;

public class UsuarioRepository
{
    public static Usuario? RealizarLogin(string email, string senha)
    {
        using var context = new MeuDiarioSenacContext();
        var usuario = context.Usuarios.FirstOrDefault(u => u.Email == email);

        if (usuario is null)
            return null;

        bool senhaValida;
        try
        {
            senhaValida = BCrypt.Net.BCrypt.Verify(senha, usuario.Senha);
        }
        catch (BCrypt.Net.SaltParseException)
        {
            // Conta criada antes da senha passar a ser salva com hash (texto puro no banco).
            senhaValida = false;
        }

        return senhaValida ? usuario : null;
    }

    public static bool CadastrarUsuario(string nome, string email, string senha)
    {
        using var context = new MeuDiarioSenacContext();
        try
        {
            var novoUsuario = new Usuario { Nome = nome, Email = email, Senha = BCrypt.Net.BCrypt.HashPassword(senha) };
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
