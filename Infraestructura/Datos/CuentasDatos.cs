using System;
using System.Data;
using Infraestructura.Conexiones;
using Infraestructura.Modelos;

namespace Infraestructura.Datos
{
    public class CuentasDatos
    {
        private ConexionDB conexion;

        public CuentasDatos(string cadenaConexion)
        {
            conexion = new ConexionDB(cadenaConexion);
        }

        public void InsertarCuenta(CuentaModel cuenta)
        {
            using var conn = conexion.GetConexion();
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                var comando = new NpgsqlCommand("INSERT INTO cuentas(id_cliente, nro_cuenta, fecha_alta, tipo_cuenta, estado, saldo, nro_contrato, costo_mantenimiento, promedio_acreditacion, moneda, estado_cuenta) " +
                                                "VALUES(@id_cliente, @nro_cuenta, @fecha_alta, @tipo_cuenta, @estado, @saldo, @nro_contrato, @costo_mantenimiento, @promedio_acreditacion, @moneda, @estado_cuenta) " +
                                                "RETURNING id_cuenta", conn, tx);

                comando.Parameters.AddWithValue("id_cliente", cuenta.IdCliente);
                comando.Parameters.AddWithValue("nro_cuenta", cuenta.NroCuenta);
                comando.Parameters.AddWithValue("fecha_alta", cuenta.FechaAlta);
                comando.Parameters.AddWithValue("tipo_cuenta", cuenta.TipoCuenta);
                comando.Parameters.AddWithValue("estado", cuenta.Estado);
                comando.Parameters.AddWithValue("saldo", cuenta.Saldo);
                comando.Parameters.AddWithValue("nro_contrato", cuenta.NroContrato);
                comando.Parameters.AddWithValue("costo_mantenimiento", cuenta.CostoMantenimiento);
                comando.Parameters.AddWithValue("promedio_acreditacion", cuenta.PromedioAcreditacion);
                comando.Parameters.AddWithValue("moneda", cuenta.Moneda);
                comando.Parameters.AddWithValue("estado_cuenta", cuenta.EstadoCuenta);

                var id = (int)comando.ExecuteScalar();

                tx.Commit();
                cuenta.IdCuenta = id;
            }
            catch (Exception)
            {
                tx.Rollback();
                throw;
            }
        }

        public CuentaModel ObtenerCuentaPorId(int id)
        {
            using var conn = conexion.GetConexion();
            conn.Open();
            using var comando = new NpgsqlCommand($"SELECT * FROM cuentas WHERE id_cuenta = {id}", conn);

            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new CuentaModel
                {
                    IdCuenta = reader.GetInt32("id_cuenta"),
                    IdCliente = reader.GetInt32("id_cliente"),
                    NroCuenta = reader.GetString("nro_cuenta"),
                    FechaAlta = reader.GetDateTime("fecha_alta"),
                    TipoCuenta = reader.GetString("tipo_cuenta"),
                    Estado = reader.GetString("estado"),
                    Saldo = reader.GetDecimal("saldo"),
                    NroContrato = reader.GetString("nro_contrato"),
                    CostoMantenimiento = reader.GetDecimal("costo_mantenimiento"),
                    PromedioAcreditacion = reader.GetDecimal("promedio_acreditacion"),
                    Moneda = reader.GetString("moneda"),
                    EstadoCuenta = reader.GetString("estado_cuenta")
                };
            }

            return null;
        }

        public void ModificarCuenta(CuentaModel cuenta)
        {
            using var conn = conexion.GetConexion();
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                var comando = new NpgsqlCommand($"UPDATE cuentas SET id_cliente = @id_cliente, nro_cuenta = @nro_cuenta, " +
                                               $"fecha_alta = @fecha_alta, tipo_cuenta = @tipo_cuenta, estado = @estado, " +
                                               $"saldo = @saldo, nro_contrato = @nro_contrato, costo_mantenimiento = @costo_mantenimiento, " +
                                               $"promedio_acreditacion = @promedio_acreditacion, moneda = @moneda, estado_cuenta = @estado_cuenta " +
                                               $"WHERE id_cuenta = {cuenta.IdCuenta}", conn, tx);

                comando.Parameters.AddWithValue("id_cliente", cuenta.IdCliente);
                comando.Parameters.AddWithValue("nro_cuenta", cuenta.NroCuenta);
                comando.Parameters.AddWithValue("fecha_alta", cuenta.FechaAlta);
                comando.Parameters.AddWithValue("tipo_cuenta", cuenta.TipoCuenta);
                comando.Parameters.AddWithValue("estado", cuenta.Estado);
                comando.Parameters.AddWithValue("saldo", cuenta.Saldo);
                comando.Parameters.AddWithValue("nro_contrato", cuenta.NroContrato);
                comando.Parameters.AddWithValue("costo_mantenimiento", cuenta.CostoMantenimiento);
                comando.Parameters.AddWithValue("promedio_acreditacion", cuenta.PromedioAcreditacion);
                comando.Parameters.AddWithValue("moneda", cuenta.Moneda);
                comando.Parameters.AddWithValue("estado_cuenta", cuenta.EstadoCuenta);

                comando.ExecuteNonQuery();
                tx.Commit();
            }
            catch (Exception)
            {
                tx.Rollback();
                throw;
            }
        }