using System.Text.RegularExpressions;

namespace MeuDiarioSenac.Business;

public class UsuarioBusiness
{
    public bool ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

    public bool ValidarSenha(string senha)
    {
        if (string.IsNullOrWhiteSpace(senha)) return false;
        return senha.Length >= 6 && Regex.IsMatch(senha, @"[0-9]");
    }
}
