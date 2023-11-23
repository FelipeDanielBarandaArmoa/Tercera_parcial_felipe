using System;
using System.Data;
using Infraestructura.Conexiones;
using Infraestructura.Modelos;

namespace Infraestructura.Datos
{
    public class ClienteDatos
    {
        private ConexionDB conexion;

        public ClienteDatos(string cadenaConexion)
        {
            conexion = new ConexionDB(cadenaConexion);
        }

        public void InsertarCliente(ClienteModel cliente)
        {
            using var conn = conexion.GetConexion();
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                var comando = new NpgsqlCommand("INSERT INTO clientes(idpersona, fechaingreso, calificacion, estado) " +
                                                "VALUES(@idpersona, @fechaingreso, @calificacion, @estado) " +
                                                "RETURNING idcliente", conn, tx);

                comando.Parameters.AddWithValue("idpersona", cliente.IdPersona);
                comando.Parameters.AddWithValue("fechaingreso", cliente.FechaIngreso);
                comando.Parameters.AddWithValue("calificacion", cliente.Calificacion);
                comando.Parameters.AddWithValue("estado", cliente.Estado);

                var id = (int)comando.ExecuteScalar();

                tx.Commit();
                cliente.IdCliente = id;
            }
            catch (Exception)
            {
                tx.Rollback();
                throw;
            }
        }

        public ClienteModel ObtenerClientePorId(int id)
        {
            using var conn = conexion.GetConexion();
            conn.Open();
            using var comando = new NpgsqlCommand($"SELECT * FROM clientes WHERE idcliente = {id}", conn);

            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new ClienteModel
                {
                    IdCliente = reader.GetInt32("idcliente"),
                    IdPersona = reader.GetInt32("idpersona"),
                    FechaIngreso = reader.GetDateTime("fechaingreso"),
                    Calificacion = reader.GetInt32("calificacion"),
                    Estado = reader.GetString("estado")
                };
            }

            return null;
        }

        public void ModificarCliente(ClienteModel cliente)
        {
            using var conn = conexion.GetConexion();
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                var comando = new NpgsqlCommand($"UPDATE clientes SET idpersona = @idpersona, fechaingreso = @fechaingreso, " +
                                               $"calificacion = @calificacion, estado = @estado " +
                                               $"WHERE idcliente = {cliente.IdCliente}", conn, tx);

                comando.Parameters.AddWithValue("idpersona", cliente.IdPersona);
                comando.Parameters.AddWithValue("fechaingreso", cliente.FechaIngreso);
                comando.Parameters.AddWithValue("calificacion", cliente.Calificacion);
                comando.Parameters.AddWithValue("estado", cliente.Estado);

                comando.ExecuteNonQuery();
                tx.Commit();
            }
            catch (Exception)
            {
                tx.Rollback();
                throw;
            }
        }

        public void EliminarCliente(int id)
        {
            using var conn = conexion.GetConexion();
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                var comando = new NpgsqlCommand($"DELETE FROM clientes WHERE idcliente = {id}", conn, tx);
                comando.ExecuteNonQuery();
                tx.Commit();
            }
            catch (Exception)
            {
                tx.Rollback();
                throw;
            }
        }
        public List<ClienteModel> ListarClientes()
        {
            var clientes = new List<ClienteModel>();

            using var conn = conexion.GetConexion();
            conn.Open();

            using var comando = new NpgsqlCommand("SELECT * FROM clientes", conn);

            using var reader = comando.ExecuteReader();
            while (reader.Read())
            {
                clientes.Add(new ClienteModel
                {
                    IdCliente = reader.GetInt32("idcliente"),
                    IdPersona = reader.GetInt32("idpersona"),
                    FechaIngreso = reader.GetDateTime("fechaingreso"),
                    Calificacion = reader.GetInt32("calificacion"),
                    Estado = reader.GetString("estado")
                });
            }

            return clientes;
        }

    }
}
