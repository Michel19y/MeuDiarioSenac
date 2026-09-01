using System;
using System.Collections.Generic;
using System.Linq;
using MeuDiarioSenac.Models;

namespace MeuDiarioSenac.Data.Repositories;

public class RegistroRepository
{
    public static void Salvar(int usuarioId, string titulo, string conteudo)
    {
        using var context = new MeuDiarioSenacContext();
        context.Registros.Add(new Registro
        {
            UsuarioId = usuarioId,
            Titulo = titulo,
            Conteudo = conteudo,
            Data = DateTime.Now
        });
        context.SaveChanges();
    }

    public static void Alterar(int id, int usuarioId, string novoTitulo, string novoConteudo)
    {
        using var context = new MeuDiarioSenacContext();
        var registro = context.Registros.FirstOrDefault(r => r.Id == id && r.UsuarioId == usuarioId);
        if (registro == null) return;

        registro.Titulo = novoTitulo;
        registro.Conteudo = novoConteudo;
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
