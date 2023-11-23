using Microsoft.AspNetCore.Mvc;
using Servicios.ContactosService;

namespace OptativoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimientosController : ControllerBase
    {
        private const string connectionString = "Host=localhost;Port=5432;User Id=postgres;Password=Century123456;Database=parcial;";
        private MovimientosService servicio;

        public MovimientosController()
        {
            servicio = new MovimientosService(connectionString);
        }

        [HttpGet("por-ruta/{id}")]
        public IActionResult ObtenerMovimientoAccion([FromRoute] int id)
        {
            var movimiento = servicio.ObtenerMovimiento(id);
            return Ok(movimiento);
        }

        [HttpGet("por-parametro")]
        public IActionResult ObtenerMovimientoAccion2([FromQuery] int id)
        {
            var movimiento = servicio.ObtenerMovimiento(id);
            return Ok(movimiento);
        }

        [HttpPost]
        public IActionResult InsertarMovimientoAccion([FromBody] Infraestructura.Modelos.MovimientosModel movimiento)
        {
            servicio.InsertarMovimiento(movimiento);
            return Created("Se creó con éxito!!", movimiento);
        }

        [HttpPut]
        public IActionResult ModificarMovimientoAccion([FromBody] Infraestructura.Modelos.MovimientosModel movimiento)
        {
            servicio.ModificarMovimiento(movimiento);
            return Ok("Se actualizó con éxito!!");
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarMovimientoAccion([FromRoute] int id)
        {
            servicio.EliminarMovimiento(id);
            return Ok("Se eliminó con éxito!!");
        }
    }
}
