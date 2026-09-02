using System;
using MeuDiarioSenac.Model;
using static MeuDiarioSenac.Views.ConsoleEstilo;

namespace MeuDiarioSenac.Views;

public class RegistroView
{
    public void ExibirCabecalhoNovoRegistro()
    {
        Console.Clear();
        EscreverLinhaColorida("╔═══════════════════════════════════════════════╗", ConsoleColor.Cyan);
        EscreverLinhaColorida("║                NOVO REGISTRO                  ║", ConsoleColor.Cyan);
        EscreverLinhaColorida("╚═══════════════════════════════════════════════╝", ConsoleColor.Cyan);
        EscreverLinhaColorida("(Digite 0 a qualquer momento para cancelar)\n", ConsoleColor.DarkGray);
    }

    public string LerTitulo()
    {
        EscreverColorido("📌 Título: ", ConsoleColor.Yellow);
        return Console.ReadLine() ?? "";
    }

    public void TituloEmBranco()
    {
        EscreverLinhaColorida("⚠️ [!] O título não pode ficar em branco.\n", ConsoleColor.Red);
    }

    public string LerConteudo()
    {
        EscreverColorido("📝 Conteúdo: ", ConsoleColor.Yellow);
        return Console.ReadLine() ?? "";
    }

    public void ConteudoEmBranco()
    {
        EscreverLinhaColorida("⚠️ [!] O conteúdo não pode ficar em branco.\n", ConsoleColor.Red);
    }

    public void RegistroSalvo()
    {
        Console.WriteLine();
        EscreverLinhaColorida("-----------------------------------------------", ConsoleColor.DarkGray);
        EscreverLinhaColorida("✅ Registro salvo no banco com sucesso!", ConsoleColor.Green);
        EscreverLinhaColorida("-----------------------------------------------", ConsoleColor.DarkGray);
        AguardarEnter();
    }

    public void ExibirCabecalhoAlteracao()
    {
        Console.Clear();
        EscreverLinhaColorida("╔═══════════════════════════════════════════════╗", ConsoleColor.Magenta);
        EscreverLinhaColorida("║              ALTERAR REGISTRO                 ║", ConsoleColor.Magenta);
        EscreverLinhaColorida("╚═══════════════════════════════════════════════╝", ConsoleColor.Magenta);
    }

    public int LerIdParaAlterar()
    {
        EscreverColorido("\n👉 Digite o ID do registro que deseja alterar (ou 0 para cancelar): ", ConsoleColor.Yellow);
        int.TryParse(Console.ReadLine(), out int registroId);
        return registroId;
    }

    public void RegistroNaoEncontrado()
    {
        EscreverLinhaColorida("\n❌ [ERRO] Registro não encontrado ou sem permissão para alterá-lo.", ConsoleColor.Red);
        EscreverColorido("Pressione ENTER para continuar...", ConsoleColor.DarkGray);
        Console.ReadLine();
    }

    public void ExibirCabecalhoNovosDados()
    {
        EscreverLinhaColorida("\n--- Digite os novos dados ---", ConsoleColor.DarkYellow);
    }

    public string LerNovoTitulo(string tituloAtual)
    {
        EscreverColorido("Novo Título ", ConsoleColor.Yellow);
        EscreverColorido($"(Atual: {tituloAtual}): ", ConsoleColor.DarkGray);
        return Console.ReadLine() ?? "";
    }

    public string LerNovoConteudo(string conteudoAtual)
    {
        EscreverColorido("Novo Conteúdo ", ConsoleColor.Yellow);
        EscreverColorido($"(Atual: {conteudoAtual}): ", ConsoleColor.DarkGray);
        return Console.ReadLine() ?? "";
    }

    public void ExibirConfirmacao(string tituloAtual, string novoTitulo, string conteudoAtual, string novoConteudo)
    {
        EscreverLinhaColorida("\n════════════════ CONFIRMAÇÃO ════════════════", ConsoleColor.DarkCyan);

        EscreverColorido("Título:   ", ConsoleColor.Gray);
        EscreverColorido($"{tituloAtual} ", ConsoleColor.DarkRed);
        EscreverColorido("--> ", ConsoleColor.Gray);
        EscreverLinhaColorida($"{novoTitulo}", ConsoleColor.Green);

        EscreverColorido("Conteúdo: ", ConsoleColor.Gray);
        EscreverColorido($"{conteudoAtual} ", ConsoleColor.DarkRed);
        EscreverColorido("--> ", ConsoleColor.Gray);
        EscreverLinhaColorida($"{novoConteudo}", ConsoleColor.Green);

        EscreverLinhaColorida("═════════════════════════════════════════════", ConsoleColor.DarkCyan);
    }

    public string LerConfirmacao()
    {
        EscreverColorido("Certeza que deseja alterar? (S/N): ", ConsoleColor.Yellow);
        return (Console.ReadLine() ?? "").Trim().ToUpper();
    }

    public void AlteracaoSucesso()
    {
        EscreverLinhaColorida("\n✅ Registro alterado com sucesso!", ConsoleColor.Green);
    }

    public void AlteracaoCancelada()
    {
        EscreverLinhaColorida("\n❌ Alteração cancelada.", ConsoleColor.Red);
    }

    public void ExibirRegistro(Registro registro)
    {
        EscreverColorido("🆔 [", ConsoleColor.DarkGray);
        EscreverColorido($"{registro.Id}", ConsoleColor.Cyan);
        EscreverColorido("] ", ConsoleColor.DarkGray);
        EscreverColorido($"📌 {registro.Titulo}", ConsoleColor.White);
        EscreverLinhaColorida($"   (📅 {registro.Data:dd/MM/yyyy HH:mm})", ConsoleColor.DarkGray);

        EscreverColorido("   💬 Conteúdo: ", ConsoleColor.Gray);
        EscreverLinhaColorida($"{registro.Conteudo}", ConsoleColor.DarkYellow);

        EscreverLinhaColorida("   ─────────────────────────────────────────", ConsoleColor.DarkGray);
    }

    public void NenhumRegistroEncontrado()
    {
        EscreverLinhaColorida("\n📭 Nenhum registro encontrado para a sua conta.", ConsoleColor.DarkYellow);
    }

    public void AguardarEnter()
    {
        EscreverColorido("\nPressione ENTER para voltar ao menu...", ConsoleColor.DarkGray);
        Console.ReadLine();
    }
}
