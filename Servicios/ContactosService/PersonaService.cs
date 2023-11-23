using System;
using Infraestructura.Datos;
using Infraestructura.Modelos;

namespace Servicios.PersonaService
{
    public class PersonaService
    {
        private PersonaDatos personaDatos;

        public PersonaService(string cadenaConexion)
        {
            personaDatos = new PersonaDatos(cadenaConexion);
        }

        public void InsertarPersona(PersonaModel persona)
        {
            ValidarDatos(persona);
            personaDatos.InsertarPersona(persona);
        }

        public PersonaModel ObtenerPersona(int id)
        {
            return personaDatos.ObtenerPersonaPorId(id);
        }

        public void ModificarPersona(PersonaModel persona)
        {
            ValidarDatos(persona);
            personaDatos.ModificarPersona(persona);
        }

        public void EliminarPersona(int id)
        {
            personaDatos.EliminarPersona(id);
        }

        private void ValidarDatos(PersonaModel persona)
        {
            if (string.IsNullOrWhiteSpace(persona.Nombre))
            {
                throw new Exception("El campo Nombre es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(persona.Apellido))
            {
                throw new Exception("El campo Apellido es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(persona.Email) || !IsValidEmail(persona.Email))
            {
                throw new Exception("El campo Email es obligatorio y debe ser una dirección de correo electrónico válida.");
            }

            if (string.IsNullOrWhiteSpace(persona.Celular))
            {
                throw new Exception("El campo Celular es obligatorio.");
            }

            if (persona.Edad < 0)
            {
                throw new Exception("La Edad debe ser un valor no negativo.");
            }

            if (persona.CiudadID <= 0)
            {
                throw new Exception("El campo CiudadID es obligatorio.");
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public void ListarPersonas()
        {
            personaDatos.ListarPersonas();
        }
    }
}
