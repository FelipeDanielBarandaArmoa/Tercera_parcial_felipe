using System;
using Infraestructura.Datos;
using Infraestructura.Modelos;

namespace Servicios.ContactosService
{
    public class MovimientosService
    {
        MovimientosDatos movimientosDatos;

        public MovimientosService(string cadenaConexion)
        {
            movimientosDatos = new MovimientoDatos(cadenaConexion);
        }

        public void InsertarMovimiento(MovimientosModel movimiento)
        {
            ValidarDatos(movimiento);
            movimientosDatos.InsertarMovimiento(movimiento);
        }

        public MovimientosModel ObtenerMovimiento(int id)
        {
            return movimientosDatos.ObtenerMovimientoPorId(id);
        }

        public void ModificarMovimiento(MovimientoModel movimiento)
        {
            ValidarDatos(movimiento);
            movimientosDatos.ModificarMovimiento(movimiento);
        }

        public void EliminarMovimiento(int id)
        {
            movimientosDatos.EliminarMovimiento(id);
        }
        private void ValidarDatos(MovimientosModel movimiento)
        {
            if (movimiento.IdCuenta <= 0)
            {
                throw new Exception("El campo IdCuenta es inválido.");
            }

            if (movimiento.MontoMovimiento < 0)
            {
                throw new Exception("El campo MontoMovimiento no puede ser negativo.");
            }

            if (string.IsNullOrEmpty(movimiento.TipoMovimiento))
            {
                throw new Exception("El campo TipoMovimiento es obligatorio y no puede estar vacío.");
            }

            if (movimiento.FechaMovimiento == default(DateTime))
            {
                throw new Exception("El campo FechaMovimiento es obligatorio.");
            }
        }

    }
}
