using Microsoft.AspNetCore.Mvc;
using Productos.Api.Controllers;

namespace Productos.Api.Tests
{
    public class McdControllerTests
    {
        [Theory]
        [InlineData(48, 18, 6)]
        [InlineData(20, 8, 4)]
        [InlineData(15, 5, 5)]
        [InlineData(25, 0, 25)]
        public void CalcularMcd_DebeRetornarMcdCorrecto(int dividendo, int divisor, int esperado)
        {
            var controller = new McdController();

            var resultado = controller.CalcularMcd(dividendo, divisor);

            var okResult = Assert.IsType<OkObjectResult>(resultado);

            Assert.NotNull(okResult.Value);

            var propiedadMcd = okResult.Value.GetType().GetProperty("mcd");

            Assert.NotNull(propiedadMcd);

            var mcd = propiedadMcd.GetValue(okResult.Value);

            Assert.Equal(esperado, mcd);
        }
    }
}
