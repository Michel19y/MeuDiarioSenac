
using System;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using SistemaRegistros;

namespace MeuDiarioSenac;

public class Menu
{
    public static Usuario? usuarioLogado = null;

    public static void ExibirMenuInicial()
    {
        string opcao = "";

        do
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE REGISTROS ===");
            Console.WriteLine("1. Logar");
            Console.WriteLine("2. Registrar (Criar Conta)");
            Console.WriteLine("3. Sair");
            Console.Write("\nEscolha uma opção: ");
            opcao = Console.ReadLine() ?? "";

            switch (opcao)
            {
                case "1":
                    Autenticacao.ExecutarLogin();
                    break;

                case "2":
                    Autenticacao.ExecutarRegistro();
                    break;

                case "3":
                    Console.WriteLine("\nSaindo do sistema... Até logo!");
                    break;

                default:
                    Console.WriteLine("\nOpção inválida! Pressione ENTER para tentar novamente...");
                    Console.ReadLine();
                    break;
            }

        } while (opcao != "3");
    }

    public static void MenuUsuario()
    {

        if (usuarioLogado == null)
        {
            Console.WriteLine("\n[ERRO] Sessão inválida. Retornando ao menu...");
            Console.ReadLine();
            return;
        }

        string opcaoMenuUsuario = "";

        do
        {
            Console.Clear();
            Console.WriteLine($"=== ÁREA DO USUÁRIO | Olá, {usuarioLogado?.Nome ?? "Usuário"} ===");
            Console.WriteLine("1. Ver registros");
            Console.WriteLine("2. Criar novo registro");
            Console.WriteLine("3. Alterar registro");
            Console.WriteLine("4. Sair (Voltar ao menu inicial)");
            Console.Write("\nEscolha uma opção: ");
            opcaoMenuUsuario = Console.ReadLine() ?? "";

            switch (opcaoMenuUsuario)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("=== MEUS REGISTROS ===");
                    Registro.ListarRegistros(usuarioLogado?.Id ?? 0);
                    Console.WriteLine("\nPressione ENTER para voltar ao menu...");
                    Console.ReadLine();
                    break;

                case "2":
                    Registro.ExecutarCriacaoRegistro();
                    break;

                case "3":
                    Registro.ExecutarAlteracaoRegistro();
                    break;

                case "4":
                    usuarioLogado = null;
                    Console.WriteLine("\nDeslogando...");
                    break;

                default:
                    Console.WriteLine("\nOpção inválida! Pressione ENTER para tentar novamente...");
                    Console.ReadLine();
                    break;
            }

        } while (opcaoMenuUsuario != "4");
    }
}