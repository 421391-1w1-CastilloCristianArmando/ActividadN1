using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiEFDBF.Models;
using WebApiEFDBF.Service;

namespace WebApiEFDBF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private IFacturaService _service;

        public FacturaController(IFacturaService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Factura? factura)
        {
            try
            {
                if (factura != null)
                {
                    _service.Create(factura);
                }
                return Ok("Factura creada.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error con el servidor.");
            }
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
                return BadRequest("Id invalido.");
            try
            {
                return Ok(_service.GetById(id));
            }
            catch (Exception)
            {
                return StatusCode(500, "Error con el servidor.");
            }
        }

        [HttpPut("id")]
        public IActionResult Update(int id, [FromBody] Factura factura)
        {
            try
            {
                var result = _service.GetById(id);
                if (result == null)
                    return NotFound("No se encontro la factura con ese id.");
                result.NroFactura = factura.NroFactura;
                result.Fecha = factura.Fecha;
                result.IdPago = factura.IdPago;
                result.Cliente = factura.Cliente;
                _service.Update(result);
                return Ok(factura);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error con el servidor.");
            }
        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok("Factura eliminada");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error con el servidor."); ;
            }
        }
    }
}
