using System.Diagnostics;
using System.Text;
using MeuDiarioSenac.Business;
using MeuDiarioSenac.Data.Repositories;
using MeuDiarioSenac.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("Frontend");

app.Use(async (context, next) =>
{
    var caminho = context.Request.Path + context.Request.QueryString;

    if (context.Request.Path.StartsWithSegments("/logs"))
    {
        await next();
        return;
    }

    var cronometro = Stopwatch.StartNew();

    context.Request.EnableBuffering();
    string? corpoRequisicao = null;
    if (context.Request.ContentLength is > 0)
    {
        using var leitor = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
        corpoRequisicao = await leitor.ReadToEndAsync();
        context.Request.Body.Position = 0;
    }

    var streamOriginal = context.Response.Body;
    await using var bufferResposta = new MemoryStream();
    context.Response.Body = bufferResposta;

    string? corpoResposta = null;
    try
    {
        await next();

        bufferResposta.Position = 0;
        corpoResposta = await new StreamReader(bufferResposta).ReadToEndAsync();
        bufferResposta.Position = 0;
        await bufferResposta.CopyToAsync(streamOriginal);
    }
    finally
    {
        context.Response.Body = streamOriginal;
        cronometro.Stop();

        ServerLogStore.Registrar(new ServerLogEntry(
            ServerLogStore.ProximoId(),
            DateTime.Now,
            context.Request.Method,
            caminho.ToString(),
            context.Response.StatusCode,
            cronometro.ElapsedMilliseconds,
            corpoRequisicao,
            corpoResposta,
            context.Connection.RemoteIpAddress?.ToString() ?? "?"
        ));
    }
});

var registroBusiness = new RegistroBusiness();
var usuarioBusiness = new UsuarioBusiness();

app.MapGet("/", () => "morra makswwon!");
app.MapGet("/vida", () => "viva makswwon!");

app.MapGet("/logs", () => Results.Ok(ServerLogStore.Listar()));
app.MapDelete("/logs", () =>
{
    ServerLogStore.Limpar();
    return Results.NoContent();
});

app.MapPost("/usuarios", (CadastroUsuarioRequest request) =>
{
    if (!usuarioBusiness.ValidarEmail(request.Email))
        return Results.BadRequest("E-mail inválido.");

    if (!usuarioBusiness.ValidarSenha(request.Senha))
        return Results.BadRequest("Senha inválida. Use ao menos 6 caracteres e 1 número.");

    var cadastrado = UsuarioRepository.CadastrarUsuario(request.Nome, request.Email, request.Senha);

    return cadastrado
        ? Results.Created($"/usuarios/{request.Email}", new { request.Nome, request.Email })
        : Results.Conflict("Não foi possível cadastrar o usuário.");
});

app.MapPost("/usuarios/login", (LoginRequest request) =>
{
    var usuario = UsuarioRepository.RealizarLogin(request.Email, request.Senha);

    return usuario is null
        ? Results.Unauthorized()
        : Results.Ok(new { usuario.Id, usuario.Nome, usuario.Email });
});

app.MapGet("/registros/{usuarioId:int}", (int usuarioId) =>
{
    try
    {
        registroBusiness.ValidarUsuario(usuarioId);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }

    return Results.Ok(RegistroRepository.ListarPorUsuario(usuarioId));
});

app.MapGet("/registros/{usuarioId:int}/{id:int}", (int usuarioId, int id) =>
{
    try
    {
        registroBusiness.ValidarBusca(id, usuarioId);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }

    var registro = RegistroRepository.BuscarPorId(id, usuarioId);
    return registro is null ? Results.NotFound() : Results.Ok(registro);
});

app.MapPost("/registros", (Registro registro) =>
{
    try
    {
        registroBusiness.ValidarNovoRegistro(registro);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }

    registro.Data = DateTime.Now;
    RegistroRepository.Salvar(registro);
    return Results.Created($"/registros/{registro.UsuarioId}/{registro.Id}", registro);
});

app.MapPut("/registros", (Registro registro) =>
{
    try
    {
        registroBusiness.ValidarAlteracaoRegistro(registro);
        var existente = RegistroRepository.BuscarPorId(registro.Id, registro.UsuarioId);
        registroBusiness.ValidarRegistroEncontrado(existente, "Registro não encontrado para este usuário.");
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
    catch (InvalidOperationException ex)
    {
        return Results.NotFound(ex.Message);
    }

    RegistroRepository.Alterar(registro);
    return Results.NoContent();
});

app.Run();

record CadastroUsuarioRequest(string Nome, string Email, string Senha);
record LoginRequest(string Email, string Senha);

record ServerLogEntry(
    int Id,
    DateTime Timestamp,
    string Metodo,
    string Caminho,
    int Status,
    long DuracaoMs,
    string? CorpoRequisicao,
    string? CorpoResposta,
    string IpOrigem);

static class ServerLogStore
{
    private const int Limite = 300;
    private static readonly object Trava = new();
    private static readonly LinkedList<ServerLogEntry> Logs = new();
    private static int contador = 0;

    public static int ProximoId() => Interlocked.Increment(ref contador);

    public static void Registrar(ServerLogEntry entry)
    {
        lock (Trava)
        {
            Logs.AddFirst(entry);
            while (Logs.Count > Limite)
                Logs.RemoveLast();
        }
    }

    public static List<ServerLogEntry> Listar()
    {
        lock (Trava)
        {
            return Logs.ToList();
        }
    }

    public static void Limpar()
    {
        lock (Trava)
        {
            Logs.Clear();
        }
    }
}
