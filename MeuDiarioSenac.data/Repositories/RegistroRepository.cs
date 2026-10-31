using System.Collections.Generic;
using System.Linq;
using MeuDiarioSenac.Model;

namespace MeuDiarioSenac.Data.Repositories;

public class RegistroRepository
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
        existente.Tags = registro.Tags;
        context.SaveChanges();
    }

    public static void Deletar(int id, int usuarioId)
    {
        using var context = new MeuDiarioSenacContext();
        var existente = context.Registros.FirstOrDefault(r => r.Id == id && r.UsuarioId == usuarioId);
        if (existente == null) return;

        context.Registros.Remove(existente);
        context.SaveChanges();
    }

    public static void AtualizarImagem(int id, int usuarioId, string imagemUrl)
    {
        using var context = new MeuDiarioSenacContext();
        var existente = context.Registros.FirstOrDefault(r => r.Id == id && r.UsuarioId == usuarioId);
        if (existente == null) return;

        existente.ImagemUrl = imagemUrl;
        context.SaveChanges();
    }

    public static List<Registro> ListarPorUsuario(int usuarioId, string? busca = null, string? tag = null)
    {
        using var context = new MeuDiarioSenacContext();
        var query = context.Registros.Where(r => r.UsuarioId == usuarioId);

        if (!string.IsNullOrWhiteSpace(busca))
        {
            query = query.Where(r => r.Titulo.Contains(busca) || r.Conteudo.Contains(busca));
        }

        if (!string.IsNullOrWhiteSpace(tag))
        {
            query = query.Where(r => r.Tags != null && r.Tags.Contains(tag));
        }

        return query.ToList();
    }

    public static Registro? BuscarPorId(int id, int usuarioId)
    {
        using var context = new MeuDiarioSenacContext();
        return context.Registros.FirstOrDefault(r => r.Id == id && r.UsuarioId == usuarioId);
    }
}
