using System;

namespace Infraestructura.Modelos
{
    public class ClienteModel
    {
        public int IdCliente { get; set; }
        public int IdPersona { get; set; }
        public DateTime FechaIngreso { get; set; }
        public required string Calificacion { get; set; }
        public required string Estado { get; set; }
    }
}