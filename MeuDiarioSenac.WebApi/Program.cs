using MeuDiarioSenac.Business;
using MeuDiarioSenac.Data.Repositories;
using MeuDiarioSenac.Model;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var registroBusiness = new RegistroBusiness();
var usuarioBusiness = new UsuarioBusiness();

app.MapGet("/", () => "morra makswwon!");
app.MapGet("/vida", () => "viva makswwon!");

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
