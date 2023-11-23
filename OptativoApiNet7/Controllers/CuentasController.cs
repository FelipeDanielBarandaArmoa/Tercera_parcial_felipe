using Microsoft.AspNetCore.Mvc;
using Servicios.ContactosService;

namespace OptativoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CuentasController : ControllerBase
    {
        private const string connectionString = "Host=localhost;Port=5432;User Id=postgres;Password=Century123456;Database=parcial;";
        private CuentasService servicio;

        public CuentasController()
        {
            servicio = new CuentasService(connectionString);
        }

        [HttpGet("por-ruta/{id}")]
        public IActionResult ObtenerCuentaAccion([FromRoute] int id)
        {
            var cuenta = servicio.ObtenerCuenta(id);
            return Ok(cuenta);
        }

        [HttpGet("por-parametro")]
        public IActionResult ObtenerCuentaAccion2([FromQuery] int id)
        {
            var cuenta = servicio.ObtenerCuenta(id);
            return Ok(cuenta);
        }

        [HttpPost]
        public IActionResult InsertarCuentaAccion([FromBody] Infraestructura.Modelos.CuentasModel cuenta)
        {
            servicio.InsertarCuenta(cuenta);
            return Created("Se creó con éxito!!", cuenta);
        }

        [HttpPut]
        public IActionResult ModificarCuentaAccion([FromBody] Infraestructura.Modelos.CuentasModel cuenta)
        {
            servicio.ModificarCuenta(cuenta);
            return Ok("Se actualizó con éxito!!");
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarCuentaAccion([FromRoute] int id)
        {
            servicio.EliminarCuenta(id);
            return Ok("Se eliminó con éxito!!");
        }
    }
}
