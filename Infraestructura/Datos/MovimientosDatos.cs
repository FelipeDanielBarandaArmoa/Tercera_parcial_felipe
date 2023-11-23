using System;
using System.Data;
using Infraestructura.Conexiones;
using Infraestructura.Modelos;

namespace Infraestructura.Datos
{
    public class MovimientosDatos
    {
        private ConexionDB conexion;

        public MovimientosDatos(string cadenaConexion)
        {
            conexion = new ConexionDB(cadenaConexion);
        }

        public void InsertarMovimiento(MovimientoModel movimiento)
        {
            using var conn = conexion.GetConexion();
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                var comando = new NpgsqlCommand("INSERT INTO movimientos(id_cuenta, fecha_movimiento, tipo_movimiento, saldo_anterior, saldo_actual, monto_movimiento, cuenta_origen, cuenta_destino, canal) " +
                                                "VALUES(@id_cuenta, @fecha_movimiento, @tipo_movimiento, @saldo_anterior, @saldo_actual, @monto_movimiento, @cuenta_origen, @cuenta_destino, @canal) " +
                                                "RETURNING id_movimiento", conn, tx);

                comando.Parameters.AddWithValue("id_cuenta", movimiento.IdCuenta);
                comando.Parameters.AddWithValue("fecha_movimiento", movimiento.FechaMovimiento);
                comando.Parameters.AddWithValue("tipo_movimiento", movimiento.TipoMovimiento);
                comando.Parameters.AddWithValue("saldo_anterior", movimiento.SaldoAnterior);
                comando.Parameters.AddWithValue("saldo_actual", movimiento.SaldoActual);
                comando.Parameters.AddWithValue("monto_movimiento", movimiento.MontoMovimiento);
                comando.Parameters.AddWithValue("cuenta_origen", movimiento.CuentaOrigen);
                comando.Parameters.AddWithValue("cuenta_destino", movimiento.CuentaDestino);
                comando.Parameters.AddWithValue("canal", movimiento.Canal);

                var id = (int)comando.ExecuteScalar();

                tx.Commit();
                movimiento.IdMovimiento = id;
            }
            catch (Exception)
            {
                tx.Rollback();
                throw;
            }
        }

        public MovimientoModel ObtenerMovimientoPorId(int id)
        {
            using var conn = conexion.GetConexion();
            conn.Open();
            using var comando = new NpgsqlCommand($"SELECT * FROM movimientos WHERE id_movimiento = {id}", conn);

            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new MovimientoModel
                {
                    IdMovimiento = reader.GetInt32("id_movimiento"),
                    IdCuenta = reader.GetInt32("id_cuenta"),
                    FechaMovimiento = reader.GetDateTime("fecha_movimiento"),
                    TipoMovimiento = reader.GetString("tipo_movimiento"),
                    SaldoAnterior = reader.GetDecimal("saldo_anterior"),
                    SaldoActual = reader.GetDecimal("saldo_actual"),
                    MontoMovimiento = reader.GetDecimal("monto_movimiento"),
                    CuentaOrigen = reader.GetString("cuenta_origen"),
                    CuentaDestino = reader.GetString("cuenta_destino"),
                    Canal = reader.GetString("canal")
                };
            }

            return null;
        }

        public void ModificarMovimiento(MovimientoModel movimiento)
        {
            using var conn = conexion.GetConexion();
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                var comando = new NpgsqlCommand($"UPDATE movimientos SET id_cuenta = @id_cuenta, fecha_movimiento = @fecha_movimiento, " +
                                               $"tipo_movimiento = @tipo_movimiento, saldo_anterior = @saldo_anterior, saldo_actual = @saldo_actual, " +
                                               $"monto_movimiento = @monto_movimiento, cuenta_origen = @cuenta_origen, cuenta_destino = @cuenta_destino, canal = @canal " +
                                               $"WHERE id_movimiento = {movimiento.IdMovimiento}", conn, tx);

                comando.Parameters.AddWithValue("id_cuenta", movimiento.IdCuenta);
                comando.Parameters.AddWithValue("fecha_movimiento", movimiento.FechaMovimiento);
                comando.Parameters.AddWithValue("tipo_movimiento", movimiento.TipoMovimiento);
                comando.Parameters.AddWithValue("saldo_anterior", movimiento.SaldoAnterior);
                comando.Parameters.AddWithValue("saldo_actual", movimiento.SaldoActual);
                comando.Parameters.AddWithValue("monto_movimiento", movimiento.MontoMovimiento);
                comando.Parameters.AddWithValue("cuenta_origen", movimiento.CuentaOrigen);
                comando.Parameters.AddWithValue("cuenta_destino", movimiento.CuentaDestino);
                comando.Parameters.AddWithValue("canal", movimiento.Canal);

                comando.ExecuteNonQuery();
                tx.Commit();
            }
            catch (Exception)
            {
                tx.Rollback();
                throw;
            }
        }

        public void EliminarMovimiento(int id)
        {
            using var conn = conexion.GetConexion();
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                var comando = new NpgsqlCommand($"DELETE FROM movimientos WHERE id_movimiento = {id}", conn, tx);
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
