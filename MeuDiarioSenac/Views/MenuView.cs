using System;
using MeuDiarioSenac.Models;

namespace MeuDiarioSenac.Views;

public class MenuView
{
    public string ExibirMenuInicial()
    {
        Console.Clear();
        Console.WriteLine("=== SISTEMA DE REGISTROS ===");
        Console.WriteLine("1. Logar");
        Console.WriteLine("2. Registrar (Criar Conta)");
        Console.WriteLine("3. Sair");
        Console.Write("\nEscolha uma opção: ");
        return Console.ReadLine() ?? "";
    }

    public void OpcaoInvalida()
    {
        Console.WriteLine("\nOpção inválida! Pressione ENTER para tentar novamente...");
        Console.ReadLine();
    }

    public void Saindo()
    {
        Console.WriteLine("\nSaindo do sistema... Até logo!");
    }

    public string ExibirMenuUsuario(Usuario usuario)
    {
        Console.Clear();
        Console.WriteLine($"=== ÁREA DO USUÁRIO | Olá, {usuario.Nome} ===");
        Console.WriteLine("1. Ver registros");
        Console.WriteLine("2. Criar novo registro");
        Console.WriteLine("3. Alterar registro");
        Console.WriteLine("4. Sair (Voltar ao menu inicial)");
        Console.Write("\nEscolha uma opção: ");
        return Console.ReadLine() ?? "";
    }

    public void SessaoInvalida()
    {
        Console.WriteLine("\n[ERRO] Sessão inválida. Retornando ao menu...");
        Console.ReadLine();
    }

    public void ExibirCabecalhoMeusRegistros()
    {
        Console.Clear();
        Console.WriteLine("=== MEUS REGISTROS ===");
    }

    public void AguardarEnter()
    {
        Console.WriteLine("\nPressione ENTER para voltar ao menu...");
        Console.ReadLine();
    }

    public void Deslogando()
    {
        Console.WriteLine("\nDeslogando...");
    }
}
