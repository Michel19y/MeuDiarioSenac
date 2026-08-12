using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using SistemaRegistros;


namespace MeuDiarioSenac.Data
{
    public class RegistroDAO
    { 
        public static void SalvarRegistro(int usuarioId, string titulo, string conteudo)
        {
            using (var conexao = MeuDiarioSenacContext.ObterConexao())
            {
                string sql = "INSERT INTO registros (usuario_id, titulo, data, conteudo) VALUES (@usuario_id, @titulo, @data, @conteudo)";
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@usuario_id", usuarioId);
                    cmd.Parameters.AddWithValue("@titulo", titulo);
                    cmd.Parameters.AddWithValue("@data", DateTime.Now);
                    cmd.Parameters.AddWithValue("@conteudo", conteudo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void AlterarRegistro(int id, int usuarioId, string novoTitulo, string novoConteudo)
        {
            using (var conexao = MeuDiarioSenacContext.ObterConexao())
            {
                string sql = "UPDATE registros SET titulo = @titulo, conteudo = @conteudo WHERE id = @id AND usuario_id = @usuario_id";
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@usuario_id", usuarioId);
                    cmd.Parameters.AddWithValue("@titulo", novoTitulo);
                    cmd.Parameters.AddWithValue("@conteudo", novoConteudo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Retorna dados brutos da query sem se preocupar com a formatação da tela
        public static MySqlDataReader ObterReaderRegistros(MySqlConnection conexao, int usuarioId)
        {
            string sql = "SELECT id, titulo, data, conteudo FROM registros WHERE usuario_id = @usuario_id";
            var cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@usuario_id", usuarioId);
            return cmd.ExecuteReader();
        }

        public static (int Id, string Titulo, string Conteudo)? BuscarPorId(int id, int usuarioId)
        {
            using (var conexao = MeuDiarioSenacContext.ObterConexao())
            {
                string sql = "SELECT id, titulo, conteudo FROM registros WHERE id = @id AND usuario_id = @usuario_id";
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@usuario_id", usuarioId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return (
                                reader.GetInt32("id"),
                                reader.GetString("titulo"),
                                reader.GetString("conteudo")
                            );
                        }
                    }
                }
            }
            return null;
        }
    }
}