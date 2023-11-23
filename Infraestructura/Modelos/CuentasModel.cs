using System;

namespace Infraestructura.Modelos
{
    public class CuentasModel
    {
        public int Id_cuenta { get; set; }
        public int id_cliente { get; set; }
        public required string NroCuenta { get; set; }
        public DateTime FechaAlta { get; set; }
        public required string TipoCuenta { get; set; }
        public required string Estado { get; set; }
        public decimal Saldo { get; set; }
        public required string nro_Contrato { get; set; }
        public decimal CostoMantenimiento { get; set; }
        public required string PromedioAcreditacion { get; set; }
        public required string Moneda { get; set; }
        public required string EstadoCuenta { get; set; }
    }
}