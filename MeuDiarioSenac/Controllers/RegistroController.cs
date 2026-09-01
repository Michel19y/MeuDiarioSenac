using MeuDiarioSenac.Data.Repositories;
using MeuDiarioSenac.Views;

namespace MeuDiarioSenac.Controllers;

public class RegistroController
{
    private readonly RegistroView view = new();

    public void ExecutarCriacaoRegistro()
    {
        view.ExibirCabecalhoNovoRegistro();

        string titulo = "";
        while (string.IsNullOrWhiteSpace(titulo))
        {
            titulo = view.LerTitulo();
            if (titulo == "0") return;

            if (string.IsNullOrWhiteSpace(titulo))
                view.TituloEmBranco();
        }

        string conteudo = "";
        while (string.IsNullOrWhiteSpace(conteudo))
        {
            conteudo = view.LerConteudo();
            if (conteudo == "0") return;

            if (string.IsNullOrWhiteSpace(conteudo))
                view.ConteudoEmBranco();
        }

        RegistroRepository.Salvar(SessaoAtual.UsuarioLogado!.Id, titulo, conteudo);

        view.RegistroSalvo();
    }

    public void ExecutarAlteracaoRegistro()
    {
        view.ExibirCabecalhoAlteracao();

        ListarRegistros(SessaoAtual.UsuarioLogado!.Id);

        int registroId = view.LerIdParaAlterar();
        if (registroId == 0) return;

        var registroAtual = RegistroRepository.BuscarPorId(registroId, SessaoAtual.UsuarioLogado.Id);

        if (registroAtual == null)
        {
            view.RegistroNaoEncontrado();
            return;
        }

        view.ExibirCabecalhoNovosDados();

        string novoTitulo = "";
        while (string.IsNullOrWhiteSpace(novoTitulo))
        {
            novoTitulo = view.LerNovoTitulo(registroAtual.Titulo);

            if (string.IsNullOrWhiteSpace(novoTitulo))
                view.TituloEmBranco();
        }

        string novoConteudo = "";
        while (string.IsNullOrWhiteSpace(novoConteudo))
        {
            novoConteudo = view.LerNovoConteudo(registroAtual.Conteudo);

            if (string.IsNullOrWhiteSpace(novoConteudo))
                view.ConteudoEmBranco();
        }

        view.ExibirConfirmacao(registroAtual.Titulo, novoTitulo, registroAtual.Conteudo, novoConteudo);

        string confirmacao = view.LerConfirmacao();

        if (confirmacao == "S")
        {
            RegistroRepository.Alterar(registroId, SessaoAtual.UsuarioLogado.Id, novoTitulo, novoConteudo);
            view.AlteracaoSucesso();
        }
        else
        {
            view.AlteracaoCancelada();
        }

        view.AguardarEnter();
    }

    public void ListarRegistros(int usuarioId)
    {
        var registros = RegistroRepository.ListarPorUsuario(usuarioId);

        if (registros.Count == 0)
        {
            view.NenhumRegistroEncontrado();
            return;
        }

        foreach (var registro in registros)
        {
            view.ExibirRegistro(registro);
        }
    }
}
