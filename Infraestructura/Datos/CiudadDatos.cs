using System;
using System.Data;
using Infraestructura.Conexiones;
using Infraestructura.Modelos;

namespace Infraestructura.Datos
{
	public class CiudadDatos
	{
		private ConexionDB conexion;

		public CiudadDatos(string cadenaConexion)
		{
           conexion = new ConexionDB(cadenaConexion);
        }

		public void insertarCiudad(CiudadModel ciudad)
		{
			var conn = conexion.GetConexion();
			var comando = new Npgsql.NpgsqlCommand("INSERT INTO ciudad(idCiudad, Descripcion, nombre_corto, estado)" +
												"VALUES(@idCiudad, @descripcion, @nombre_corto, @estado)", conn);
			comando.Parameters.AddWithValue("descripcion", ciudad.descripcion);
			comando.Parameters.AddWithValue("nombre_corto", ciudad.nombre_corto);
			comando.Parameters.AddWithValue("estado", ciudad.estado);
			comando.Parameters.AddWithValue("idCiudad", ciudad.idCiudad);

			comando.ExecuteNonQuery();
		}

		public CiudadModel obtenerCiudadPorId(int id)
		{
			var conn = conexion.GetConexion();
			var comando = new Npgsql.NpgsqlCommand($"Select * from ciudad where idCiudad = {id}", conn);
			using var reader = comando.ExecuteReader();
			if (reader.Read())
			{
				return new CiudadModel
				{
					idCiudad = reader.GetInt32("idCiudad"),
					descripcion = reader.GetString("descripcion"),
					nombre_corto = reader.GetString("nombre_corto"),
					estado = reader.GetString("estado")
				};
			}
			return null;
		}

		public void modificarCiudad(CiudadModel ciudad)
		{
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"UPDATE ciudad SET Descripcion = '{ciudad.descripcion}', " +
                                                          $"nombre_corto = '{ciudad.nombre_corto}', " +
                                                          $"estado = '{ciudad.estado}' " +
                                                $" WHERE idCiudad = {ciudad.idCiudad}", conn);

            comando.ExecuteNonQuery();
        }
	}
}

