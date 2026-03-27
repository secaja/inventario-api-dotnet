using inventario.API.Data;
using inventario.API.DTOS;
using inventario.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace inventario.API.Controllers
{
    [ApiController]
    [Route("productos")]
    public class ProductosController : ControllerBase
    {
        private readonly InventarioDbContext _context;

        public ProductosController(InventarioDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("inventario")]
        public async Task<IActionResult> ObtenerInventario()
        {
            var productos = await _context.Productos.ToListAsync();
            return Ok(productos);
        }

        [Authorize]
        [HttpPost("movimiento")]
        public async Task<IActionResult> Movimiento([FromBody] MovimientoDTO movimiento)
        {
            if (string.IsNullOrEmpty(movimiento.Nombre))
                return BadRequest("El nombre es obligatorio");

            if (movimiento.Cantidad <= 0)
                return BadRequest("La cantidad debe ser mayor a 0");

            var tipo = movimiento.Tipo.ToLower().Trim();

            var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.Nombre == movimiento.Nombre);

            if (producto == null)
            {
                producto = new Producto
                {
                    Nombre = movimiento.Nombre,
                    Cantidad = 0
                };

                _context.Productos.Add(producto);
            }

            if (tipo == "entrada")
            {
                producto.Cantidad += movimiento.Cantidad;
            }
            else if (tipo == "salida")
            {
                if (producto.Cantidad < movimiento.Cantidad)
                    return BadRequest("Stock insuficiente");

                producto.Cantidad -= movimiento.Cantidad;
            }
            else
            {
                return BadRequest("Tipo inválido (entrada/salida)");
            }

            await _context.SaveChangesAsync();

            return Ok(producto);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                return NotFound("Producto no encontrado");

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return Ok("Producto eliminado");
        }
    }
}
