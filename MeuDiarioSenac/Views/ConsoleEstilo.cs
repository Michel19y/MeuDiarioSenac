using System;

namespace MeuDiarioSenac.Views;

internal static class ConsoleEstilo
{
    public static void EscreverColorido(string mensagem, ConsoleColor cor)
    {
        Console.ForegroundColor = cor;
        Console.Write(mensagem);
        Console.ResetColor();
    }

    public static void EscreverLinhaColorida(string mensagem, ConsoleColor cor)
    {
        Console.ForegroundColor = cor;
        Console.WriteLine(mensagem);
        Console.ResetColor();
    }
}
