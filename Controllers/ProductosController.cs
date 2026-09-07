using Dapper;
using Microsoft.AspNetCore.Mvc;
using Productos.Api.Data;
using Productos.Api.Models;

namespace Productos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly DbConnection _db;

    public ProductosController(DbConnection db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductos()
    {
        using var connection = _db.CreateConnection();

        var sql = "SELECT Id, Nombre, Precio, Stock FROM Productos";

        var productos = await connection.QueryAsync<Producto>(sql);

        return Ok(productos);
    }

    [HttpPost]
    public async Task<IActionResult> CrearProducto(Producto producto)
    {
    using var connection = _db.CreateConnection();

    var sql = @"
        INSERT INTO Productos (Nombre, Precio, Stock)
        VALUES (@Nombre, @Precio, @Stock);
        
        SELECT CAST(SCOPE_IDENTITY() AS INT);
    ";

    var id = await connection.QuerySingleAsync<int>(sql, producto);

    producto.Id = id;

    return CreatedAtAction(
        nameof(GetProductos),
        new { id = producto.Id },
        producto
    );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProducto(int id)
    {
    using var connection = _db.CreateConnection();

    var sql = @"
        SELECT Id, Nombre, Precio, Stock
        FROM Productos
        WHERE Id = @Id
    ";

    var producto = await connection.QuerySingleOrDefaultAsync<Producto>(
        sql,
        new { Id = id }
    );

    if (producto == null)
    {
        return NotFound(new
        {
            mensaje = "Producto no encontrado"
        });
    }

    return Ok(producto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarProducto(int id, Producto producto)
    {
    using var connection = _db.CreateConnection();

    var sql = @"
        UPDATE Productos
        SET Nombre = @Nombre,
            Precio = @Precio,
            Stock = @Stock
        WHERE Id = @Id;
    ";

    var filas = await connection.ExecuteAsync(sql, new
    {
        Id = id,
        producto.Nombre,
        producto.Precio,
        producto.Stock
    });

    if (filas == 0)
    {
        return NotFound(new
        {
            mensaje = "Producto no encontrado"
        });
    }

    return Ok(new
    {
        mensaje = "Producto actualizado correctamente"
    });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarProducto(int id)
    {
    using var connection = _db.CreateConnection();

    var sql = "DELETE FROM Productos WHERE Id = @Id";

    var filas = await connection.ExecuteAsync(
        sql,
        new { Id = id }
    );

    if (filas == 0)
    {
        return NotFound(new
        {
            mensaje = "Producto no encontrado"
        });
    }

    return Ok(new
    {
        mensaje = "Producto eliminado correctamente"
    });
    }

}
