using System;

namespace Infraestructura.Modelos
{
    public class PersonaModel
    {
        public int Id { get; set; }
        public int IdCiudad { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; }
    }
}
