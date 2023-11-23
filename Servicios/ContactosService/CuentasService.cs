using System;
using Infraestructura.Datos;
using Infraestructura.Modelos;

namespace Servicios.ContactosService
{
    public class CuentasService
    {
        CuentasDatos cuentasDatos;

        public CuentasService(string cadenaConexion)
        {
            cuentasDatos = new CuentasDatos(cadenaConexion);
        }

        public void InsertarCuenta(CuentasModel cuenta)
        {
            ValidarDatos(cuenta);
            cuentasDatos.InsertarCuenta(cuenta);
        }

        public CuentasModel ObtenerCuenta(int id)
        {
            return cuentasDatos.ObtenerCuentaPorId(id);
        }

        public void ModificarCuenta(CuentasModel cuenta)
        {
            ValidarDatos(cuenta);
            cuentasDatos.ModificarCuenta(cuenta);
        }

        public void EliminarCuenta(int id)
        {
            cuentasDatos.EliminarCuenta(id);
        }

        private void ValidarDatos(CuentasModel cuenta)
        {
            if (cuenta.IdCliente <= 0)
            {
                throw new Exception("El campo IdCliente es inválido.");
            }

            if (string.IsNullOrEmpty(cuenta.NroCuenta))
            {
                throw new Exception("El campo NroCuenta es obligatorio y no puede estar vacío.");
            }

        }
    }
}
