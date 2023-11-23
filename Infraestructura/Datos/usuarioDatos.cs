using System;
using System.Data;
using Infraestructura.Conexiones;
using Infraestructura.Modelos;

namespace Infraestructura.Datos
{
    public class UsuarioDatos
    {
        private ConexionDB conexion;

        public UsuarioDatos(string cadenaConexion)
        {
            conexion = new ConexionDB(cadenaConexion);
        }

        public void InsertarUsuario(UsuarioModel usuario)
        {
            using var conn = conexion.GetConexion();
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                var comando = new NpgsqlCommand("INSERT INTO usuarios(idpersona, nombre_usuario, Contraseña, nivel, estado) " +
                                                "VALUES(@idpersona, @nombre_usuario, @Contraseña, @nivel, @estado)", conn, tx);

                comando.Parameters.AddWithValue("idpersona", usuario.IdPersona);
                comando.Parameters.AddWithValue("nombre_usuario", usuario.NombreUsuario);
                comando.Parameters.AddWithValue("Contraseña", usuario.Contraseña);
                comando.Parameters.AddWithValue("nivel", usuario.Nivel);
                comando.Parameters.AddWithValue("estado", usuario.Estado);

                comando.ExecuteNonQuery();
                tx.Commit();
            }
            catch (Exception)
            {
                tx.Rollback();
                throw;
            }
        }

        public UsuarioModel ObtenerUsuarioPorId(int idUsuario)
        {
            using var conn = conexion.GetConexion();
            conn.Open();
            using var comando = new NpgsqlCommand($"SELECT * FROM usuarios WHERE idusuario = {idUsuario}", conn);

            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new UsuarioModel
                {
                    IdUsuario = reader.GetInt32("idusuario"),
                    IdPersona = reader.GetInt32("idpersona"),
                    NombreUsuario = reader.GetString("nombre_usuario"),
                    Contraseña = reader.GetString("Contraseña"),
                    Nivel = reader.GetString("nivel"),
                    Estado = reader.GetString("estado")
                };
            }

            return null;
        }

        public void ModificarUsuario(UsuarioModel usuario)
        {
            using var conn = conexion.GetConexion();
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                var comando = new NpgsqlCommand($"UPDATE usuarios SET idpersona = @idpersona, " +
                                               $"nombre_usuario = @nombre_usuario, Contraseña = @Contraseña, " +
                                               $"nivel = @nivel, estado = @estado " +
                                               $"WHERE idusuario = {usuario.IdUsuario}", conn, tx);

                comando.Parameters.AddWithValue("idpersona", usuario.IdPersona);
                comando.Parameters.AddWithValue("nombre_usuario", usuario.NombreUsuario);
                comando.Parameters.AddWithValue("Contraseña", usuario.Contraseña);
                comando.Parameters.AddWithValue("nivel", usuario.Nivel);
                comando.Parameters.AddWithValue("estado", usuario.Estado);

                comando.ExecuteNonQuery();
                tx.Commit();
            }
            catch (Exception)
            {
                tx.Rollback();
                throw;
            }
        }
        public UsuarioModel ObtenerUsuarioPorCredenciales(string nombreUsuario, string Contraseña)
        {
            using var conn = conexion.GetConexion();
            conn.Open();
            using var comando = new NpgsqlCommand("SELECT * FROM usuarios WHERE nombre_usuario = @nombre_usuario AND Contraseña = @Contraseña", conn);

            comando.Parameters.AddWithValue("nombre_usuario", nombreUsuario);
            comando.Parameters.AddWithValue("Contraseña", Contraseña);

            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new UsuarioModel
                {
                    IdUsuario = reader.GetInt32("idusuario"),
                    IdPersona = reader.GetInt32("idpersona"),
                    NombreUsuario = reader.GetString("nombre_usuario"),
                    Contraseña = reader.GetString("Contraseña"),
                    Nivel = reader.GetString("nivel"),
                    Estado = reader.GetString("estado")
                };
            }

            return null;
        }


        public void EliminarUsuario(int idUsuario)
        {
            using var conn = conexion.GetConexion();
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                var comando = new NpgsqlCommand($"DELETE FROM usuarios WHERE idusuario = {idUsuario}", conn, tx);
                comando.ExecuteNonQuery();
                tx.Commit();
            }
            catch (Exception)
            {
                tx.Rollback();
                throw;
            }
        }
    }
}
