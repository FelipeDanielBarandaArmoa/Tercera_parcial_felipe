using Microsoft.AspNetCore.Mvc;
using Servicios.ContactosService;

namespace OptativoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private const string connectionString = "Host=localhost;Port=5432;User Id = postgres;Password=Century123456;Database=parcial;";
        private ClienteService servicio; 

        public ClienteController()
        {
            servicio = new ClienteService(connectionString);
        }

        [HttpGet("por-ruta/{id}")]
        public IActionResult ObtenerClienteAccion([FromRoute] int id)
        {
            var cliente = servicio.ObtenerCliente(id);
            return Ok(cliente);
        }

        [HttpGet("por-parametro")]
        public IActionResult ObtenerClienteAccion2([FromQuery] int id)
        {
            var cliente = servicio.ObtenerCliente(id);
            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult InsertarClienteAccion([FromBody] Infraestructura.Modelos.ClienteModel cliente)
        {
            servicio.InsertarCliente(cliente);
            return Created("Se creó con éxito!!", cliente);
        }

        [HttpPut]
        public IActionResult ModificarClienteAccion([FromBody] Infraestructura.Modelos.ClienteModel cliente)
        {
            servicio.ModificarCliente(cliente);
            return Ok("Se actualizó con éxito!!");
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarClienteAccion([FromRoute] int id)
        {
            servicio.EliminarCliente(id);
            return Ok("Se eliminó con éxito!!");
        }
    }
}
