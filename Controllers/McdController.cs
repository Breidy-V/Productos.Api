using Microsoft.AspNetCore.Mvc;

namespace Productos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class McdController : ControllerBase
    {
        [HttpGet]
        public IActionResult CalcularMcd(int dividendo, int divisor)
        {
            while (divisor != 0)
            {
                int residuo = dividendo % divisor;
                dividendo = divisor;
                divisor = residuo;
            }

            return Ok(new
            {
                mcd = dividendo
            });
        }
    }
}

