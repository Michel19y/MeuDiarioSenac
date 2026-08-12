using System;
using MySql.Data.MySqlClient;
using MeuDiarioSenac.Data;



namespace MeuDiarioSenac;

public class Registro
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public string Conteudo { get; set; } = string.Empty;

    public static void ExecutarCriacaoRegistro()
    {
        Console.Clear();
        EscreverLinhaColorida("╔═══════════════════════════════════════════════╗", ConsoleColor.Cyan);
        EscreverLinhaColorida("║                NOVO REGISTRO                  ║", ConsoleColor.Cyan);
        EscreverLinhaColorida("╚═══════════════════════════════════════════════╝", ConsoleColor.Cyan);
        EscreverLinhaColorida("(Digite 0 a qualquer momento para cancelar)\n", ConsoleColor.DarkGray);

        string titulo = "";
        while (string.IsNullOrWhiteSpace(titulo))
        {
            EscreverColorido("📌 Título: ", ConsoleColor.Yellow);
            titulo = Console.ReadLine() ?? "";
            if (titulo == "0") return;

            if (string.IsNullOrWhiteSpace(titulo))
                EscreverLinhaColorida("⚠️ [!] O título não pode ficar em branco.\n", ConsoleColor.Red);
        }

        string conteudo = "";
        while (string.IsNullOrWhiteSpace(conteudo))
        {
            EscreverColorido("📝 Conteúdo: ", ConsoleColor.Yellow);
            conteudo = Console.ReadLine() ?? "";
            if (conteudo == "0") return;

            if (string.IsNullOrWhiteSpace(conteudo))
                EscreverLinhaColorida("⚠️ [!] O conteúdo não pode ficar em branco.\n", ConsoleColor.Red);
        }

        // Chama o DAO para salvar no MySQL
        RegistroDAO.SalvarRegistro(Menu.usuarioLogado!.Id, titulo, conteudo);

        Console.WriteLine();
        EscreverLinhaColorida("-----------------------------------------------", ConsoleColor.DarkGray);
        EscreverLinhaColorida("✅ Registro salvo no banco com sucesso!", ConsoleColor.Green);
        EscreverLinhaColorida("-----------------------------------------------", ConsoleColor.DarkGray);

        EscreverColorido("\nPressione ENTER para voltar ao menu...", ConsoleColor.DarkGray);
        Console.ReadLine();
    }

    public static void ExecutarAlteracaoRegistro()
    {
        Console.Clear();
        EscreverLinhaColorida("╔═══════════════════════════════════════════════╗", ConsoleColor.Magenta);
        EscreverLinhaColorida("║              ALTERAR REGISTRO                 ║", ConsoleColor.Magenta);
        EscreverLinhaColorida("╚═══════════════════════════════════════════════╝", ConsoleColor.Magenta);

        ListarRegistros(Menu.usuarioLogado!.Id);

        EscreverColorido("\n👉 Digite o ID do registro que deseja alterar (ou 0 para cancelar): ", ConsoleColor.Yellow);
        if (!int.TryParse(Console.ReadLine(), out int registroId) || registroId == 0)
            return;

        // Busca o registro atual via DAO
        var registroAtual = RegistroDAO.BuscarPorId(registroId, Menu.usuarioLogado.Id);

        if (registroAtual == null)
        {
            EscreverLinhaColorida("\n❌ [ERRO] Registro não encontrado ou sem permissão para alterá-lo.", ConsoleColor.Red);
            EscreverColorido("Pressione ENTER para continuar...", ConsoleColor.DarkGray);
            Console.ReadLine();
            return;
        }

        EscreverLinhaColorida("\n--- Digite os novos dados ---", ConsoleColor.DarkYellow);

        string novoTitulo = "";
        while (string.IsNullOrWhiteSpace(novoTitulo))
        {
            EscreverColorido($"Novo Título ", ConsoleColor.Yellow);
            EscreverColorido($"(Atual: {registroAtual.Value.Titulo}): ", ConsoleColor.DarkGray);
            novoTitulo = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(novoTitulo))
                EscreverLinhaColorida("⚠️ [!] O título não pode ficar em branco.\n", ConsoleColor.Red);
        }

        string novoConteudo = "";
        while (string.IsNullOrWhiteSpace(novoConteudo))
        {
            EscreverColorido($"Novo Conteúdo ", ConsoleColor.Yellow);
            EscreverColorido($"(Atual: {registroAtual.Value.Conteudo}): ", ConsoleColor.DarkGray);
            novoConteudo = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(novoConteudo))
                EscreverLinhaColorida("⚠️ [!] O conteúdo não pode ficar em branco.\n", ConsoleColor.Red);
        }

        EscreverLinhaColorida("\n════════════════ CONFIRMAÇÃO ════════════════", ConsoleColor.DarkCyan);

        EscreverColorido("Título:   ", ConsoleColor.Gray);
        EscreverColorido($"{registroAtual.Value.Titulo} ", ConsoleColor.DarkRed);
        EscreverColorido("--> ", ConsoleColor.Gray);
        EscreverLinhaColorida($"{novoTitulo}", ConsoleColor.Green);

        EscreverColorido("Conteúdo: ", ConsoleColor.Gray);
        EscreverColorido($"{registroAtual.Value.Conteudo} ", ConsoleColor.DarkRed);
        EscreverColorido("--> ", ConsoleColor.Gray);
        EscreverLinhaColorida($"{novoConteudo}", ConsoleColor.Green);

        EscreverLinhaColorida("═════════════════════════════════════════════", ConsoleColor.DarkCyan);

        EscreverColorido("Certeza que deseja alterar? (S/N): ", ConsoleColor.Yellow);
        string confirmacao = (Console.ReadLine() ?? "").Trim().ToUpper();

        if (confirmacao == "S")
        {
            // Chama o DAO para executar a alteração no MySQL
            RegistroDAO.AlterarRegistro(registroId, Menu.usuarioLogado.Id, novoTitulo, novoConteudo);
            EscreverLinhaColorida("\n✅ Registro alterado com sucesso!", ConsoleColor.Green);
        }
        else
        {
            EscreverLinhaColorida("\n❌ Alteração cancelada.", ConsoleColor.Red);
        }

        EscreverColorido("\nPressione ENTER para voltar ao menu...", ConsoleColor.DarkGray);
        Console.ReadLine();
    }

    public static void ListarRegistros(int usuarioId)
    {
        using (var conexao = MeuDiarioSenacContext.ObterConexao())
        {
            using (var reader = RegistroDAO.ObterReaderRegistros(conexao, usuarioId))
            {
                bool encontrou = false;
                while (reader.Read())
                {
                    encontrou = true;
                    int id = reader.GetInt32("id");
                    string titulo = reader.GetString("titulo");
                    DateTime data = reader.GetDateTime("data");
                    string conteudo = reader.GetString("conteudo");

                    // Card estilizado para cada registro
                    EscreverColorido("🆔 [", ConsoleColor.DarkGray);
                    EscreverColorido($"{id}", ConsoleColor.Cyan);
                    EscreverColorido("] ", ConsoleColor.DarkGray);
                    EscreverColorido($"📌 {titulo}", ConsoleColor.White);
                    EscreverLinhaColorida($"  (📅 {data:dd/MM/yyyy HH:mm})", ConsoleColor.DarkGray);

                    EscreverColorido("   💬 Conteúdo: ", ConsoleColor.Gray);
                    EscreverLinhaColorida($"{conteudo}", ConsoleColor.DarkYellow);

                    EscreverLinhaColorida("   ─────────────────────────────────────────", ConsoleColor.DarkGray);
                }

                if (!encontrou)
                {
                    EscreverLinhaColorida("\n📭 Nenhum registro encontrado para a sua conta.", ConsoleColor.DarkYellow);
                }
            }
        }
    }

    // --- MÉTODOS AUXILIARES DE ESTILIZAÇÃO DE COR ---
    private static void EscreverColorido(string mensagem, ConsoleColor cor)
    {
        Console.ForegroundColor = cor;
        Console.Write(mensagem);
        Console.ResetColor();
    }

    private static void EscreverLinhaColorida(string mensagem, ConsoleColor cor)
    {
        Console.ForegroundColor = cor;
        Console.WriteLine(mensagem);
        Console.ResetColor();
    }
}