using System;
using Infraestructura.Datos;
using Infraestructura.Modelos;

namespace Servicios.ContactosService
{
    public class ClienteService
    {
        ClienteDatos clienteDatos;

        public ClienteService(string cadenaConexion)
        {
            clienteDatos = new ClienteDatos(cadenaConexion);
        }

        public void InsertarCliente(ClienteModel cliente)
        {
            ValidarDatos(cliente);
            clienteDatos.InsertarCliente(cliente);
        }

        public ClienteModel ObtenerCliente(int id)
        {
            return clienteDatos.ObtenerClientePorId(id);
        }

        public void ModificarCliente(ClienteModel cliente)
        {
            ValidarDatos(cliente);
            clienteDatos.ModificarCliente(cliente);
        }

        public void EliminarCliente(int id)
        {
            clienteDatos.EliminarCliente(id);
        }

        private void ValidarDatos(ClienteModel cliente)
        {
            if (cliente.IdCliente <= 0)
            {
                throw new Exception("El campo IdCliente es inválido.");
            }

            if (cliente.FechaIngreso == default(DateTime))
            {
                throw new Exception("El campo FechaIngreso es obligatorio.");
            }
        }
        public void ListarClientes()
        {
            clienteDatos.ListarClientes();
        }
}
