using System;
using static MeuDiarioSenac.Views.ConsoleEstilo;

namespace MeuDiarioSenac.Views;

public class AutenticacaoView
{
    public void ExibirCabecalhoLogin()
    {
        Console.Clear();
        EscreverLinhaColorida("╔═══════════════════════════════════════════════╗", ConsoleColor.Blue);
        EscreverLinhaColorida("║                     LOGIN                     ║", ConsoleColor.Blue);
        EscreverLinhaColorida("╚═══════════════════════════════════════════════╝", ConsoleColor.Blue);
        EscreverLinhaColorida("(Digite 0 a qualquer momento para voltar)\n", ConsoleColor.DarkGray);
    }

    public string LerEmail()
    {
        EscreverColorido("📧 Digite seu e-mail: ", ConsoleColor.Yellow);
        return Console.ReadLine() ?? "";
    }

    public string LerSenha()
    {
        EscreverColorido("🔒 Digite sua senha: ", ConsoleColor.Yellow);
        return Console.ReadLine() ?? "";
    }

    public void EmailInvalido()
    {
        EscreverLinhaColorida("\n❌ [ERRO] Formato de e-mail inválido! Exemplo: usuario@email.com", ConsoleColor.Red);
        EscreverColorido("Pressione ENTER para tentar novamente...", ConsoleColor.DarkGray);
        Console.ReadLine();
    }

    public void CredenciaisInvalidas()
    {
        EscreverLinhaColorida("\n❌ [ERRO] E-mail ou senha incorretos.", ConsoleColor.Red);
        EscreverColorido("Pressione ENTER para tentar novamente...", ConsoleColor.DarkGray);
        Console.ReadLine();
    }

    public void ExibirCabecalhoRegistro()
    {
        Console.Clear();
        EscreverLinhaColorida("╔═══════════════════════════════════════════════╗", ConsoleColor.Yellow);
        EscreverLinhaColorida("║               CRIAR NOVA CONTA                ║", ConsoleColor.Yellow);
        EscreverLinhaColorida("╚═══════════════════════════════════════════════╝", ConsoleColor.Yellow);
        EscreverLinhaColorida("(Digite 0 a qualquer momento para cancelar)\n", ConsoleColor.DarkGray);
    }

    public string LerNome()
    {
        EscreverColorido("🙍 Nome completo: ", ConsoleColor.Yellow);
        return Console.ReadLine() ?? "";
    }

    public void NomeInvalido()
    {
        EscreverLinhaColorida("⚠️ [!] O nome não pode ser vazio.\n", ConsoleColor.Red);
    }

    public string LerEmailCadastro()
    {
        EscreverColorido("📧 E-mail: ", ConsoleColor.Yellow);
        return Console.ReadLine() ?? "";
    }

    public void EmailInvalidoCadastro()
    {
        EscreverLinhaColorida("⚠️ [!] E-mail inválido! Exemplo: 'nome@email.com'.\n", ConsoleColor.Red);
    }

    public string LerSenhaCadastro()
    {
        EscreverColorido("🔒 Senha (Mínimo 6 caracteres e pelo menos 1 número): ", ConsoleColor.Yellow);
        return Console.ReadLine() ?? "";
    }

    public void SenhaInvalida()
    {
        EscreverLinhaColorida("⚠️ [!] Senha fraca! Mínimo de 6 caracteres e 1 número.\n", ConsoleColor.Red);
    }

    public void CadastroSucesso()
    {
        EscreverLinhaColorida("\n✅ Conta criada com sucesso! Você já pode fazer login.", ConsoleColor.Green);
    }

    public void CadastroErro()
    {
        EscreverLinhaColorida("\n❌ [ERRO] Não foi possível cadastrar a conta.", ConsoleColor.Red);
    }

    public void AguardarEnter()
    {
        EscreverColorido("\nPressione ENTER para voltar ao menu principal...", ConsoleColor.DarkGray);
        Console.ReadLine();
    }
}
