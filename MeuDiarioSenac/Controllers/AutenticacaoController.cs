using System.Text.RegularExpressions;
using MeuDiarioSenac.Data.Repositories;
using MeuDiarioSenac.Views;

namespace MeuDiarioSenac.Controllers;

public class AutenticacaoController
{
    private readonly AutenticacaoView view = new();

    public static bool ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

    public static bool ValidarSenha(string senha)
    {
        if (string.IsNullOrWhiteSpace(senha)) return false;
        return senha.Length >= 6 && Regex.IsMatch(senha, @"[0-9]");
    }

    public void ExecutarLogin()
    {
        while (true)
        {
            view.ExibirCabecalhoLogin();

            string email = view.LerEmail();
            if (email == "0") return;

            if (!ValidarEmail(email))
            {
                view.EmailInvalido();
                continue;
            }

            string senha = view.LerSenha();
            if (senha == "0") return;

            SessaoAtual.UsuarioLogado = UsuarioRepository.RealizarLogin(email, senha);

            if (SessaoAtual.UsuarioLogado == null)
            {
                view.CredenciaisInvalidas();
                continue;
            }

            new MenuController().MenuUsuario();
            break;
        }
    }

    public void ExecutarRegistro()
    {
        view.ExibirCabecalhoRegistro();

        string nome = "";
        while (string.IsNullOrWhiteSpace(nome))
        {
            nome = view.LerNome();
            if (nome == "0") return;

            if (string.IsNullOrWhiteSpace(nome))
                view.NomeInvalido();
        }

        string email = "";
        while (true)
        {
            email = view.LerEmailCadastro();
            if (email == "0") return;

            if (!ValidarEmail(email))
            {
                view.EmailInvalidoCadastro();
                continue;
            }
            break;
        }

        string senha = "";
        while (true)
        {
            senha = view.LerSenhaCadastro();
            if (senha == "0") return;

            if (!ValidarSenha(senha))
            {
                view.SenhaInvalida();
                continue;
            }
            break;
        }

        if (UsuarioRepository.CadastrarUsuario(nome, email, senha))
            view.CadastroSucesso();
        else
            view.CadastroErro();

        view.AguardarEnter();
    }
}
