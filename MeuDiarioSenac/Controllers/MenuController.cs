using MeuDiarioSenac.Views;

namespace MeuDiarioSenac.Controllers;

public class MenuController
{
    private readonly MenuView view = new();

    public void ExibirMenuInicial()
    {
        string opcao = "";

        do
        {
            opcao = view.ExibirMenuInicial();

            switch (opcao)
            {
                case "1":
                    new AutenticacaoController().ExecutarLogin();
                    break;

                case "2":
                    new AutenticacaoController().ExecutarRegistro();
                    break;

                case "3":
                    view.Saindo();
                    break;

                default:
                    view.OpcaoInvalida();
                    break;
            }

        } while (opcao != "3");
    }

    public void MenuUsuario()
    {
        var usuario = SessaoAtual.UsuarioLogado;
        if (usuario == null)
        {
            view.SessaoInvalida();
            return;
        }

        string opcaoMenuUsuario = "";

        do
        {
            opcaoMenuUsuario = view.ExibirMenuUsuario(usuario);

            switch (opcaoMenuUsuario)
            {
                case "1":
                    view.ExibirCabecalhoMeusRegistros();
                    new RegistroController().ListarRegistros(usuario.Id);
                    view.AguardarEnter();
                    break;

                case "2":
                    new RegistroController().ExecutarCriacaoRegistro();
                    break;

                case "3":
                    new RegistroController().ExecutarAlteracaoRegistro();
                    break;

                case "4":
                    SessaoAtual.UsuarioLogado = null;
                    view.Deslogando();
                    break;

                default:
                    view.OpcaoInvalida();
                    break;
            }

        } while (opcaoMenuUsuario != "4");
    }
}
