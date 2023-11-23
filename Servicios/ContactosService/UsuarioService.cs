using Infraestructura.Datos; 
using Infraestructura.Modelos; 
using System;
using System.Collections.Generic;
using Infraestructura.Conexiones;

namespace Servicios.ContactosService
{
    public class UsuarioService
    {
        private readonly UsuarioDatos _usuarioDatos;

        public UsuarioService(string cadenaConexion)
        {
            _usuarioDatos = new UsuarioDatos(cadenaConexion);
        }

        public void InsertarUsuario(UsuarioModel usuario)
        {
            
            ValidarUsuario(usuario);

            
            _usuarioDatos.InsertarUsuario(usuario);
        }

        public UsuarioModel ObtenerUsuarioPorId(int id)
        {
            return _usuarioDatos.ObtenerUsuarioPorId(id);
        }

        public void ModificarUsuario(UsuarioModel usuario)
        {
            
            ValidarUsuario(usuario);

            
            _usuarioDatos.ModificarUsuario(usuario);
        }

        public void EliminarUsuario(int id)
        {
            
            _usuarioDatos.EliminarUsuario(id);
        }

        public UsuarioModel ObtenerPorCredenciales(string nombreUsuario, string contrasena)
        {
            return _usuarioDatos.ObtenerPorCredenciales(nombreUsuario, contrasena);
        }

        public List<UsuarioModel> ObtenerTodosLosUsuarios()
        {
            return _usuarioDatos.ObtenerTodosLosUsuarios();
        }

        private void ValidarUsuario(UsuarioModel usuario)
        {
            
            if (string.IsNullOrEmpty(usuario.NombreUsuario))
            {
                throw new ArgumentException("El nombre de usuario es obligatorio.");
            }

            if (string.IsNullOrEmpty(usuario.Contrasena))
            {
                throw new ArgumentException("La contraseña es obligatoria.");
            }

            
        }
    }
}
