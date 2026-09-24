using System;
using MeuDiarioSenac.Model;
using static MeuDiarioSenac.Views.ConsoleEstilo;

namespace MeuDiarioSenac.Views;

public class MenuView
{
    public string ExibirMenuInicial()
    {
        Console.Clear();
        EscreverLinhaColorida("╔═══════════════════════════════════════════════╗", ConsoleColor.Cyan);
        EscreverLinhaColorida("║              SISTEMA DE REGISTROS             ║", ConsoleColor.Cyan);
        EscreverLinhaColorida("╚═══════════════════════════════════════════════╝", ConsoleColor.Cyan);
        Console.WriteLine();
        EscreverLinhaColorida("1. 🔑 Logar", ConsoleColor.White);
        EscreverLinhaColorida("2. 🆕 Registrar (Criar Conta)", ConsoleColor.White);
        EscreverLinhaColorida("3. 🚪 Sair", ConsoleColor.White);
        EscreverColorido("\nEscolha uma opção: ", ConsoleColor.Yellow);
        return Console.ReadLine() ?? "";
    }

    public void OpcaoInvalida()
    {
        EscreverLinhaColorida("\n⚠️ Opção inválida! Pressione ENTER para tentar novamente...", ConsoleColor.Red);
        Console.ReadLine();
    }

    public void Saindo()
    {
        EscreverLinhaColorida("\n👋 Saindo do sistema... Até logo!", ConsoleColor.Cyan);
    }

    public string ExibirMenuUsuario(Usuario usuario)
    {
        Console.Clear();
        EscreverLinhaColorida("╔═══════════════════════════════════════════════╗", ConsoleColor.Green);
        EscreverLinhaColorida("║                ÁREA DO USUÁRIO                ║", ConsoleColor.Green);
        EscreverLinhaColorida("╚═══════════════════════════════════════════════╝", ConsoleColor.Green);
        EscreverColorido("👤 Olá, ", ConsoleColor.Gray);
        EscreverLinhaColorida(usuario.Nome, ConsoleColor.Yellow);
        Console.WriteLine();
        EscreverLinhaColorida("1. 📖 Ver registros", ConsoleColor.White);
        EscreverLinhaColorida("2. 📝 Criar novo registro", ConsoleColor.White);
        EscreverLinhaColorida("3. ✏️  Alterar registro", ConsoleColor.White);
        EscreverLinhaColorida("4. 🚪 Sair (Voltar ao menu inicial)", ConsoleColor.White);
        EscreverColorido("\nEscolha uma opção: ", ConsoleColor.Yellow);
        return Console.ReadLine() ?? "";
    }

    public void SessaoInvalida()
    {
        EscreverLinhaColorida("\n❌ [ERRO] Sessão inválida. Retornando ao menu...", ConsoleColor.Red);
        Console.ReadLine();
    }

    public void ExibirCabecalhoMeusRegistros()
    {
        Console.Clear();
        EscreverLinhaColorida("╔═══════════════════════════════════════════════╗", ConsoleColor.Cyan);
        EscreverLinhaColorida("║                MEUS REGISTROS                 ║", ConsoleColor.Cyan);
        EscreverLinhaColorida("╚═══════════════════════════════════════════════╝", ConsoleColor.Cyan);
    }

    public void AguardarEnter()
    {
        EscreverColorido("\nPressione ENTER para voltar ao menu...", ConsoleColor.DarkGray);
        Console.ReadLine();
    }

    public void Deslogando()
    {
        EscreverLinhaColorida("\n👋 Deslogando...", ConsoleColor.Yellow);
    }
}
