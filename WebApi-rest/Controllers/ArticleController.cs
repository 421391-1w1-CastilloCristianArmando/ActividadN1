using ActividadN1.Domain;
using ActividadN1.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private IArticleService _service;

        public ArticleController(IArticleService service)
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
            var article = _service.GetById(id);
            return article is null ? NotFound() : Ok(article);
        }

        [HttpPost]
        public IActionResult Save([FromBody] Article value)
        {
            if (value == null)
                return BadRequest();
            var article = _service.Save(value);
            return article ?
                StatusCode(StatusCodes.Status201Created, value)
                : Problem("No se pudo guardar el articulo.");
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Article value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            value.Id = id;
            var up = _service.GetById(id);
            if (up is null)
                return NotFound();
            var ok = _service.Update(value);
            return ok ? NoContent() : Problem("No se pudo actualizar el articulo.");
        }

        [HttpDelete("{id:int}")]

        public IActionResult Delete(int id)
        {
            var ok = _service.Detele(id);
            return ok ? NoContent(): NotFound();
        }




    }
}
