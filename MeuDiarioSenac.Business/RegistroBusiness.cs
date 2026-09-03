using System;
using System.Collections.Generic;
using MeuDiarioSenac.Data.Repositories;
using MeuDiarioSenac.Model;

namespace MeuDiarioSenac.Business;

public class RegistroBusiness
{
    public void CriarRegistro(Registro registro)
    {
        try
        {
            if (registro == null)
                throw new ArgumentException("O registro não pode ser nulo.");

            if (string.IsNullOrWhiteSpace(registro.Titulo))
                throw new ArgumentException("O título do registro não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(registro.Conteudo))
                throw new ArgumentException("O conteúdo do registro não pode ser vazio.");

            if (registro.UsuarioId <= 0)
                throw new ArgumentException("O registro precisa estar associado a um usuário válido.");

            registro.Data = DateTime.Now;

            RegistroRepository.Salvar(registro);
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Erro ao criar o registro.", ex);
        }
    }

    public void AlterarRegistro(Registro registro)
    {
        try
        {
            if (registro == null)
                throw new ArgumentException("O registro não pode ser nulo.");

            if (registro.Id <= 0)
                throw new ArgumentException("Id do registro inválido.");

            if (string.IsNullOrWhiteSpace(registro.Titulo))
                throw new ArgumentException("O título do registro não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(registro.Conteudo))
                throw new ArgumentException("O conteúdo do registro não pode ser vazio.");

            var existente = RegistroRepository.BuscarPorId(registro.Id, registro.UsuarioId);
            if (existente == null)
                throw new InvalidOperationException("Registro não encontrado para este usuário.");

            RegistroRepository.Alterar(registro);
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (InvalidOperationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Erro ao alterar o registro.", ex);
        }
    }

    public List<Registro> ListarRegistrosPorUsuario(int usuarioId)
    {
        try
        {
            if (usuarioId <= 0)
                throw new ArgumentException("Usuário inválido.");

            return RegistroRepository.ListarPorUsuario(usuarioId);
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Erro ao listar os registros do usuário.", ex);
        }
    }

    public Registro BuscarRegistroPorId(int id, int usuarioId)
    {
        try
        {
            if (id <= 0)
                throw new ArgumentException("Id do registro inválido.");

            if (usuarioId <= 0)
                throw new ArgumentException("Usuário inválido.");

            var registro = RegistroRepository.BuscarPorId(id, usuarioId);
            if (registro == null)
                throw new InvalidOperationException("Registro não encontrado.");

            return registro;
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (InvalidOperationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Erro ao buscar o registro.", ex);
        }
    }
}
