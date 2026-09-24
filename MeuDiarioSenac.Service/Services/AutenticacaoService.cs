using MeuDiarioSenac.Business;
using MeuDiarioSenac.Data.Repositories;
using MeuDiarioSenac.Views;

namespace MeuDiarioSenac.Service;

public class AutenticacaoService
{
    private readonly AutenticacaoView view = new();
    private readonly UsuarioBusiness business = new();

    public void ExecutarLogin()
    {
        while (true)
        {
            view.ExibirCabecalhoLogin();

            string email = view.LerEmail();
            if (email == "0") return;

            if (!business.ValidarEmail(email))
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

            new MenuService().MenuUsuario();
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

            if (!business.ValidarEmail(email))
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

            if (!business.ValidarSenha(senha))
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
