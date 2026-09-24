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
}
