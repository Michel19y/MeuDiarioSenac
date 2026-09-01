using System;

namespace MeuDiarioSenac.Views;

public class AutenticacaoView
{
    public void ExibirCabecalhoLogin()
    {
        Console.Clear();
        Console.WriteLine("=== LOGIN ===");
        Console.WriteLine("(Digite 0 a qualquer momento para voltar)\n");
    }

    public string LerEmail()
    {
        Console.Write("Digite seu e-mail: ");
        return Console.ReadLine() ?? "";
    }

    public string LerSenha()
    {
        Console.Write("Digite sua senha: ");
        return Console.ReadLine() ?? "";
    }

    public void EmailInvalido()
    {
        Console.WriteLine("\n[ERRO] Formato de e-mail inválido! Exemplo: usuario@email.com");
        Console.WriteLine("Pressione ENTER para tentar novamente...");
        Console.ReadLine();
    }

    public void CredenciaisInvalidas()
    {
        Console.WriteLine("\n[ERRO] E-mail ou senha incorretos.");
        Console.WriteLine("Pressione ENTER para tentar novamente...");
        Console.ReadLine();
    }

    public void ExibirCabecalhoRegistro()
    {
        Console.Clear();
        Console.WriteLine("=== CRIAR NOVA CONTA ===");
        Console.WriteLine("(Digite 0 a qualquer momento para cancelar)\n");
    }

    public string LerNome()
    {
        Console.Write("Nome completo: ");
        return Console.ReadLine() ?? "";
    }

    public void NomeInvalido()
    {
        Console.WriteLine("[!] O nome não pode ser vazio.\n");
    }

    public string LerEmailCadastro()
    {
        Console.Write("E-mail: ");
        return Console.ReadLine() ?? "";
    }

    public void EmailInvalidoCadastro()
    {
        Console.WriteLine("[!] E-mail inválido! Exemplo: 'nome@email.com'.\n");
    }

    public string LerSenhaCadastro()
    {
        Console.Write("Senha (Mínimo 6 caracteres e pelo menos 1 número): ");
        return Console.ReadLine() ?? "";
    }

    public void SenhaInvalida()
    {
        Console.WriteLine("[!] Senha fraca! Mínimo de 6 caracteres e 1 número.\n");
    }

    public void CadastroSucesso()
    {
        Console.WriteLine("\n✅ Conta criada com sucesso! Você já pode fazer login.");
    }

    public void CadastroErro()
    {
        Console.WriteLine("\n[ERRO] Não foi possível cadastrar a conta.");
    }

    public void AguardarEnter()
    {
        Console.WriteLine("\nPressione ENTER para voltar ao menu principal...");
        Console.ReadLine();
    }
}
