using System;
using Npgsql;
namespace Infraestructura.Conexiones
{
	public class ConexionDB
	{
		private string cadenaConexion;
		private NpgsqlConnection conexion;

		public ConexionDB(string cadenaConexion)
		{
			this.cadenaConexion = cadenaConexion;
			conexion = new NpgsqlConnection(cadenaConexion);
		}

		public NpgsqlConnection GetConexion()
		{

			if(conexion.State != System.Data.ConnectionState.Open)
				conexion.Open();
			return conexion;
		}
	}
}

