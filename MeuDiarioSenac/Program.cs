using MeuDiarioSenac.Service;

namespace MeuDiarioSenac;

class Program
{
    static void Main(string[] args)
    {
        new MenuService().ExibirMenuInicial();
    }
}
