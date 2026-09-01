using System;

namespace MeuDiarioSenac.Models;

public class Registro
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public string Conteudo { get; set; } = string.Empty;
}
