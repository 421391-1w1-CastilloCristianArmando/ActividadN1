using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebApiEFDBF.Models;
using WebApiEFDBF.Service;

namespace WebApiEFDBF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private IArticuloService _service;

        public ArticuloController(IArticuloService service)
        {
            _service = service;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            try
            {
                return Ok(_service.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(500, "Error con el servidor.");
            }
        }

        [HttpGet("id")]
        public IActionResult GetById([FromQuery] int id)
        {
            if (id <= 0)
            {
                return BadRequest("El id ingresado debe ser mayor a 0.");
            }
            try
            {
                return Ok(_service.GetById(id));
            }
            catch (Exception)
            {
                return StatusCode(500, "Error con el servidor.");
            }
        }

        [HttpPost]

        public IActionResult Create([FromBody] Articulo? articulo)
        {
            try
            {
                if (articulo != null)
                {
                    _service.Create(articulo);
                }
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error con el servidor.");
            }
        }

        [HttpPut("id")]

        public IActionResult Update(int id, [FromBody] Articulo articulo)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("El id debe ser mayor a 0");
                if (articulo == null)
                    return BadRequest("Faltan datos por cargar.");

                var result = _service.GetById(id);

                if (result == null)
                    return NotFound("No hay libro con ese id.");
                
                result.Nombre = articulo.Nombre;
                result.Precio = articulo.Precio;

                _service.Update(result);
                return Ok(articulo);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error con el servidor.");
            }
        }

        [HttpDelete("id")]

        public IActionResult Delete([FromQuery] int id)
        {
            try
            {                            
                _service.Delete(id);
                return Ok("Articulo Eliminado.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error con el servidor.");
            }
        }
    }
}
