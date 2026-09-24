using System;
using System.Collections.Generic;
using MeuDiarioSenac.Business;
using MeuDiarioSenac.Data.Repositories;
using MeuDiarioSenac.Model;
using MeuDiarioSenac.Views;

namespace MeuDiarioSenac.Service;

public class RegistroService
{
    private readonly RegistroView view = new();
    private readonly RegistroBusiness business = new();

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

        var registro = new Registro
        {
            UsuarioId = SessaoAtual.UsuarioLogado!.Id,
            Titulo = titulo,
            Conteudo = conteudo
        };

        try
        {
            business.ValidarNovoRegistro(registro);
            registro.Data = DateTime.Now;
            RegistroRepository.Salvar(registro);
            view.RegistroSalvo();
        }
        catch (Exception ex)
        {
            view.ExibirErro(ex.Message);
        }
    }

    public void ExecutarAlteracaoRegistro()
    {
        view.ExibirCabecalhoAlteracao();

        ListarRegistros(SessaoAtual.UsuarioLogado!.Id);

        int registroId = view.LerIdParaAlterar();
        if (registroId == 0) return;

        Registro? registroEncontrado;
        try
        {
            business.ValidarBusca(registroId, SessaoAtual.UsuarioLogado.Id);
            registroEncontrado = RegistroRepository.BuscarPorId(registroId, SessaoAtual.UsuarioLogado.Id);
            business.ValidarRegistroEncontrado(registroEncontrado, "Registro não encontrado.");
        }
        catch (Exception)
        {
            view.RegistroNaoEncontrado();
            return;
        }

        var registroAtual = registroEncontrado!;

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
            var registroAtualizado = new Registro
            {
                Id = registroId,
                UsuarioId = SessaoAtual.UsuarioLogado.Id,
                Titulo = novoTitulo,
                Conteudo = novoConteudo
            };

            try
            {
                business.ValidarAlteracaoRegistro(registroAtualizado);

                var existente = RegistroRepository.BuscarPorId(registroAtualizado.Id, registroAtualizado.UsuarioId);
                business.ValidarRegistroEncontrado(existente, "Registro não encontrado para este usuário.");

                RegistroRepository.Alterar(registroAtualizado);
                view.AlteracaoSucesso();
            }
            catch (Exception ex)
            {
                view.ExibirErro(ex.Message);
            }
        }
        else
        {
            view.AlteracaoCancelada();
        }

        view.AguardarEnter();
    }

    public void ListarRegistros(int usuarioId)
    {
        List<Registro> registros;
        try
        {
            business.ValidarUsuario(usuarioId);
            registros = RegistroRepository.ListarPorUsuario(usuarioId);
        }
        catch (Exception ex)
        {
            view.ExibirErro(ex.Message);
            return;
        }

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
