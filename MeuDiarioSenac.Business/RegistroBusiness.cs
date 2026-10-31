using System;
using MeuDiarioSenac.Model;

namespace MeuDiarioSenac.Business;

public class RegistroBusiness
{
    public void ValidarNovoRegistro(Registro registro)
    {
        if (registro == null)
            throw new ArgumentException("O registro não pode ser nulo.");

        if (string.IsNullOrWhiteSpace(registro.Titulo))
            throw new ArgumentException("O título do registro não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(registro.Conteudo))
            throw new ArgumentException("O conteúdo do registro não pode ser vazio.");

        if (registro.UsuarioId <= 0)
            throw new ArgumentException("O registro precisa estar associado a um usuário válido.");
    }

    public void ValidarAlteracaoRegistro(Registro registro)
    {
        if (registro == null)
            throw new ArgumentException("O registro não pode ser nulo.");

        if (registro.Id <= 0)
            throw new ArgumentException("Id do registro inválido.");

        if (string.IsNullOrWhiteSpace(registro.Titulo))
            throw new ArgumentException("O título do registro não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(registro.Conteudo))
            throw new ArgumentException("O conteúdo do registro não pode ser vazio.");
    }

    public void ValidarUsuario(int usuarioId)
    {
        if (usuarioId <= 0)
            throw new ArgumentException("Usuário inválido.");
    }

    public void ValidarBusca(int id, int usuarioId)
    {
        if (id <= 0)
            throw new ArgumentException("Id do registro inválido.");

        if (usuarioId <= 0)
            throw new ArgumentException("Usuário inválido.");
    }

    public void ValidarRegistroEncontrado(Registro? registro, string mensagemErro)
    {
        if (registro == null)
            throw new InvalidOperationException(mensagemErro);
    }

    private static readonly string[] TiposImagemPermitidos = { "image/jpeg", "image/png", "image/webp" };
    private const long TamanhoMaximoImagemBytes = 5 * 1024 * 1024;

    public void ValidarImagem(string? contentType, long tamanho)
    {
        if (string.IsNullOrWhiteSpace(contentType) || !TiposImagemPermitidos.Contains(contentType))
            throw new ArgumentException("Formato de imagem inválido. Use JPEG, PNG ou WEBP.");

        if (tamanho <= 0 || tamanho > TamanhoMaximoImagemBytes)
            throw new ArgumentException("A imagem deve ter no máximo 5MB.");
    }

    public EstatisticasResponse MontarEstatisticas(List<Registro> registros)
    {
        var porMes = registros
            .GroupBy(r => new DateTime(r.Data.Year, r.Data.Month, 1))
            .OrderBy(g => g.Key)
            .Select(g => new ContagemMes(g.Key.ToString("MM/yyyy"), g.Count()))
            .ToList();

        var porTag = registros
            .Where(r => !string.IsNullOrWhiteSpace(r.Tags))
            .SelectMany(r => r.Tags!.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            .GroupBy(tag => tag, StringComparer.OrdinalIgnoreCase)
            .OrderByDescending(g => g.Count())
            .Select(g => new ContagemTag(g.Key, g.Count()))
            .ToList();

        return new EstatisticasResponse(registros.Count, porMes, porTag);
    }
}
