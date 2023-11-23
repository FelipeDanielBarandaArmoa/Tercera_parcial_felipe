namespace Infraestructura.Modelos
{
    public class UsuarioModel
    {
        public int IDUsuario { get; set; }
        public int IDPersona { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Nivel { get; set; }
        public string Estado { get; set; }
        
    }
}
