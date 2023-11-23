using System;
using System.Data;
using Infraestructura.Conexiones;
using Infraestructura.Modelos;

namespace Infraestructura.Datos
{
    public class PersonaDatos
    {
        private ConexionDB conexion;

        public PersonaDatos(string cadenaConexion)
        {
            conexion = new ConexionDB(cadenaConexion);
        }

        public void InsertarPersona(PersonaModel persona)
        {
            using var conn = conexion.GetConexion();
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                var comando = new NpgsqlCommand("INSERT INTO persona(nombre, apellido, direccion, email, celular, edad, ciudad_id) " +
                                                "VALUES(@nombre, @apellido, @direccion, @email, @celular, @edad, @ciudad_id) " +
                                                "RETURNING id", conn, tx);

                comando.Parameters.AddWithValue("nombre", persona.Nombre);
                comando.Parameters.AddWithValue("apellido", persona.Apellido);
                comando.Parameters.AddWithValue("direccion", persona.Direccion);
                comando.Parameters.AddWithValue("email", persona.Email);
                comando.Parameters.AddWithValue("celular", persona.Celular);
                comando.Parameters.AddWithValue("edad", persona.Edad);
                comando.Parameters.AddWithValue("ciudad_id", persona.CiudadID);

                var id = (int)comando.ExecuteScalar();

                tx.Commit();
                persona.ID = id;
            }
            catch (Exception)
            {
                tx.Rollback();
                throw;
            }
        }

        public PersonaModel ObtenerPersonaPorId(int id)
        {
            using var conn = conexion.GetConexion();
            conn.Open();
            using var comando = new NpgsqlCommand($"SELECT * FROM persona WHERE id = {id}", conn);

            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new PersonaModel
                {
                    ID = reader.GetInt32("id"),
                    Nombre = reader.GetString("nombre"),
                    Apellido = reader.GetString("apellido"),
                    Direccion = reader.GetString("direccion"),
                    Email = reader.GetString("email"),
                    Celular = reader.GetString("celular"),
                    Edad = reader.GetInt32("edad"),
                    CiudadID = reader.GetInt32("ciudad_id")
                };
            }

            return null;
        }

        public void ModificarPersona(PersonaModel persona)
        {
            using var conn = conexion.GetConexion();
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                var comando = new NpgsqlCommand($"UPDATE persona SET nombre = @nombre, apellido = @apellido, " +
                                               $"direccion = @direccion, email = @email, celular = @celular, " +
                                               $"edad = @edad, ciudad_id = @ciudad_id " +
                                               $"WHERE id = {persona.ID}", conn, tx);

                comando.Parameters.AddWithValue("nombre", persona.Nombre);
                comando.Parameters.AddWithValue("apellido", persona.Apellido);
                comando.Parameters.AddWithValue("direccion", persona.Direccion);
                comando.Parameters.AddWithValue("email", persona.Email);
                comando.Parameters.AddWithValue("celular", persona.Celular);
                comando.Parameters.AddWithValue("edad", persona.Edad);
                comando.Parameters.AddWithValue("ciudad_id", persona.CiudadID);

                comando.ExecuteNonQuery();
                tx.Commit();
            }
            catch (Exception)
            {
                tx.Rollback();
                throw;
            }
        }

        public void EliminarPersona(int id)
        {
            using var conn = conexion.GetConexion();
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                var comando = new NpgsqlCommand($"DELETE FROM persona WHERE id = {id}", conn, tx);
                comando.ExecuteNonQuery();
                tx.Commit();
            }
            catch (Exception)
            {
                tx.Rollback();
                throw;
            }
        }
        public List<PersonaModel> ListarPersonas()
        {
            var personas = new List<PersonaModel>();

            using var conn = conexion.GetConexion();
            conn.Open();

            using var comando = new NpgsqlCommand("SELECT * FROM persona", conn);

            using var reader = comando.ExecuteReader();
            while (reader.Read())
            {
                personas.Add(new PersonaModel
                {
                    ID = reader.GetInt32("id"),
                    Nombre = reader.GetString("nombre"),
                    Apellido = reader.GetString("apellido"),
                    Direccion = reader.GetString("direccion"),
                    Email = reader.GetString("email"),
                    Celular = reader.GetString("celular"),
                    Edad = reader.GetInt32("edad"),
                    CiudadID = reader.GetInt32("ciudad_id")
                });
            }

            return personas;
        }

    }
}
