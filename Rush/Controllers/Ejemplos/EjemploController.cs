using Microsoft.AspNetCore.Mvc;
using Rush.Application.Interfaces.Ejemplo;
using Rush.Domain.DTO.Ejemplo;
using Rush.Domain.Entities.Ejemplo;
using Rush.WebAPI.Controllers.BaseGeneric;

namespace Rush.WebAPI.Controllers.Ejemplos
{
    [Route("api/[controller]")]
    [ApiController]
    public class EjemploController : BaseController<EjemploTabla, EjemploTablaDTO>
    {
        private readonly IEjemploService _service;
        public EjemploController(IEjemploService service)
             : base(service)
        {
            _service = service;
        }

        [HttpGet("GetByNombre/{nombre}")]
        public async Task<ActionResult> GetByNombre(string nombre)
        {
            var result = await _service.GetByNombreAsync(nombre);
            if (result.Data == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}