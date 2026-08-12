using System;
using System.Text.RegularExpressions;
using SistemaRegistros; 
using MeuDiarioSenac.Data;
using MySql.Data.MySqlClient; 
namespace MeuDiarioSenac;

public class Autenticacao
{
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

    public static void ExecutarLogin()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== LOGIN ===");
            Console.WriteLine("(Digite 0 a qualquer momento para voltar)\n");

            Console.Write("Digite seu e-mail: ");
            string email = Console.ReadLine() ?? "";
            if (email == "0") return;

            if (!ValidarEmail(email))
            {
                Console.WriteLine("\n[ERRO] Formato de e-mail inválido! Exemplo: usuario@email.com");
                Console.WriteLine("Pressione ENTER para tentar novamente...");
                Console.ReadLine();
                continue;
            }

            Console.Write("Digite sua senha: ");
            string senha = Console.ReadLine() ?? "";
            if (senha == "0") return;

            // Atribui diretamente à sessão global no Menu
            Menu.usuarioLogado = RealizarLogin(email, senha);

            if (Menu.usuarioLogado == null)
            {
                Console.WriteLine("\n[ERRO] E-mail ou senha incorretos.");
                Console.WriteLine("Pressione ENTER para tentar novamente...");
                Console.ReadLine();
                continue;
            }

            // Chama o menu principal do usuário logado
            Menu.MenuUsuario();
            break;
        }
    }

    public static void ExecutarRegistro()
    {
        Console.Clear();
        Console.WriteLine("=== CRIAR NOVA CONTA ===");
        Console.WriteLine("(Digite 0 a qualquer momento para cancelar)\n");

        string nome = "";
        while (string.IsNullOrWhiteSpace(nome))
        {
            Console.Write("Nome completo: ");
            nome = Console.ReadLine() ?? "";
            if (nome == "0") return;

            if (string.IsNullOrWhiteSpace(nome))
                Console.WriteLine("[!] O nome não pode ser vazio.\n");
        }

        string email = "";
        while (true)
        {
            Console.Write("E-mail: ");
            email = Console.ReadLine() ?? "";
            if (email == "0") return;

            if (!ValidarEmail(email))
            {
                Console.WriteLine("[!] E-mail inválido! Exemplo: 'nome@email.com'.\n");
                continue;
            }
            break;
        }

        string senha = "";
        while (true)
        {
            Console.Write("Senha (Mínimo 6 caracteres e pelo menos 1 número): ");
            senha = Console.ReadLine() ?? "";
            if (senha == "0") return;

            if (!ValidarSenha(senha))
            {
                Console.WriteLine("[!] Senha fraca! Mínimo de 6 caracteres e 1 número.\n");
                continue;
            }
            break;
        }

        if (CadastrarUsuario(nome, email, senha))
        {
            Console.WriteLine("\n✅ Conta criada com sucesso! Você já pode fazer login.");
        }

        Console.WriteLine("\nPressione ENTER para voltar ao menu principal...");
        Console.ReadLine();
    }

    private static Usuario? RealizarLogin(string email, string senha)
    {
        using (var conexao = MeuDiarioSenacContext.ObterConexao())
        {
            string sql = "SELECT id, nome, email, senha FROM usuarios WHERE email = @email AND senha = @senha";
            using (var cmd = new MySqlCommand(sql, conexao))
            {
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@senha", senha);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Usuario
                        {
                            Id = reader.GetInt32("id"),
                            Nome = reader.GetString("nome"),
                            Email = reader.GetString("email"),
                            Senha = reader.GetString("senha")
                        };
                    }
                }
            }
        }
        return null;
    }

    private static bool CadastrarUsuario(string nome, string email, string senha)
    {
        using (var conexao = MeuDiarioSenacContext.ObterConexao())
        {
            try
            {
                string sql = "INSERT INTO usuarios (nome, email, senha) VALUES (@nome, @email, @senha)";
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                    Console.WriteLine("\n[ERRO] Este e-mail já está cadastrado.");
                else
                    Console.WriteLine($"\n[ERRO] Falha ao cadastrar: {ex.Message}");

                return false;
            }
        }
    }
}