using System.Collections.Generic;
using System.Linq;
using MeuDiarioSenac.Model;

namespace MeuDiarioSenac.Data.Repositories;

internal class RegistroRepository
{
    public static void Salvar(Registro registro)
    {
        using var context = new MeuDiarioSenacContext();
        context.Registros.Add(registro);
        context.SaveChanges();
    }

    public static void Alterar(Registro registro)
    {
        using var context = new MeuDiarioSenacContext();
        var existente = context.Registros.FirstOrDefault(r => r.Id == registro.Id && r.UsuarioId == registro.UsuarioId);
        if (existente == null) return;

        existente.Titulo = registro.Titulo;
        existente.Conteudo = registro.Conteudo;
        context.SaveChanges();
    }

    public static List<Registro> ListarPorUsuario(int usuarioId)
    {
        using var context = new MeuDiarioSenacContext();
        return context.Registros
            .Where(r => r.UsuarioId == usuarioId)
            .ToList();
    }

    public static Registro? BuscarPorId(int id, int usuarioId)
    {
        using var context = new MeuDiarioSenacContext();
        return context.Registros.FirstOrDefault(r => r.Id == id && r.UsuarioId == usuarioId);
    }
}
