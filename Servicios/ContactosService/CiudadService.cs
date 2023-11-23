using System;
using Infraestructura.Datos;
using Infraestructura.Modelos;

namespace Servicios.ContactosService
{
	public class CiudadService
	{
		CiudadDatos ciudadDatos;

		public CiudadService(string cadenaConexion)
		{
			ciudadDatos = new CiudadDatos(cadenaConexion);
		}

		public void insertarCiudad(CiudadModel ciudad)
		{
			validarDatos(ciudad);
			ciudadDatos.insertarCiudad(ciudad);
		}

		public CiudadModel obtenerCiudad(int id)
		{
			return ciudadDatos.obtenerCiudadPorId(id);
		}

        public void modificarCiudad(CiudadModel ciudad)
        {
            validarDatos(ciudad);
            ciudadDatos.modificarCiudad(ciudad);
        }
        private void validarDatos(CiudadModel ciudad)
		{
			if(ciudad.descripcion.Trim().Length < 2)
			{
				throw new Exception("La descripción debe tener al menos 2 caracteres");
			}
		}

	}
}

