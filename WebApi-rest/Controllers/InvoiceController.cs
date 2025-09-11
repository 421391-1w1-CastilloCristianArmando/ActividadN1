using ActividadN1.Domain;
using ActividadN1.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private IInvoiceService _service;

        private bool IsOrderValid(Invoice invoice)
        {
            return true;
        }

        public InvoiceController(IInvoiceService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() 
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var ok = _service.GetById(id);
            return ok is null ? NotFound() : Ok(ok);
        }

        [HttpPost]

        public IActionResult Save([FromBody] Invoice? invoice)
        {
            try
            {
                if (invoice == null || !IsOrderValid(invoice))
                {
                    return BadRequest(new { Mensaje = "Orden de produccion incorrecta!" });
                }
                if (_service.Save(invoice))
                {
                    return Ok(new { Mensaje = "Order registrada con exito." });
                }
                else
                {
                    throw new Exception("Error de transaccion!");
                }

            }
            catch (Exception ex)
            {

                StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = ex.ToString() });
            }
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(int id, [FromBody] Invoice invoice, [FromQuery] UpdateMode mode = UpdateMode.HeaderOnly)
        {
            if (invoice is null) 
                return BadRequest("Body requerido.");

            invoice.Id = id;
            
            var current = _service.GetById(id);
            if (current is null) 
                return NotFound();
            
            if (mode == UpdateMode.HeaderAndDetails && (invoice.Details is null || invoice.Details.Count == 0))
            {
                return BadRequest("Para HeaderAndDetails, envíe la colección de detalles.");
            }

            var ok = _service.Update(invoice, mode);
            return ok ? NoContent() : Problem("No se pudo actualizar la factura.");
        }

        
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var ok = _service.Delete(id);
            return ok ? NoContent() : NotFound();
        }

    }
}
