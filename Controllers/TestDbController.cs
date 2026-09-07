using Microsoft.AspNetCore.Mvc;
using Dapper;
using Productos.Api.Data;

namespace Productos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestDbController : ControllerBase
{
    private readonly DbConnection _db;

    public TestDbController(DbConnection db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> TestConnection()
    {
        try
        {
            using var connection = _db.CreateConnection();

            var result = await connection.QuerySingleAsync<int>(
                "SELECT 1"
            );

            return Ok(new
            {
                mensaje = "Conexión exitosa con Azure SQL",
                resultado = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                mensaje = "Error al conectar con la base de datos",
                error = ex.Message
            });
        }
    }
}