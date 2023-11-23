using Microsoft.AspNetCore.Mvc;
using Servicios.PersonaService;

namespace OptativoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonaController : ControllerBase
    {
        private const string connectionString = "Host=localhost;Port=5432;User Id = postgres;Password=Century123456;Database=parcial;";
        private PersonaService servicio;

        public PersonaController()
        {
            servicio = new PersonaService(connectionString); 
        }

        [HttpGet("por-ruta/{id}")]
        public IActionResult ObtenerPersonaPorId([FromRoute] int id)
        {
            var persona = servicio.ObtenerPersona(id);
            if (persona == null)
            {
                return NotFound();
            }
            return Ok(persona);
        }

        [HttpGet("por-parametro")]
        public IActionResult ObtenerPersonaPorIdParametro([FromQuery] int id)
        {
            var persona = servicio.ObtenerPersona(id);
            if (persona == null)
            {
                return NotFound();
            }
            return Ok(persona);
        }

        [HttpPost]
        public IActionResult InsertarPersona([FromBody] Infraestructura.Modelos.PersonaModel persona)
        {
            servicio.InsertarPersona(persona);
            return Created("Se creó con éxito!!", persona);
        }

        [HttpPut]
        public IActionResult ModificarPersona([FromBody] Infraestructura.Modelos.PersonaModel persona)
        {
            var existe = servicio.ObtenerPersona(persona.ID);
            if (existe == null)
            {
                return NotFound();
            }
            servicio.ModificarPersona(persona);
            return Ok("Se actualizó con éxito!!");
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarPersona([FromRoute] int id)
        {
            var persona = servicio.ObtenerPersona(id);
            if (persona == null)
            {
                return NotFound();
            }
            servicio.EliminarPersona(id);
            return Ok("Se eliminó con éxito!!");
        }
    }
}
