using DTOs;
using Microsoft.AspNetCore.Mvc;
using SistemaProveeduriaCORE;

namespace SistemaProveeduriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        [HttpGet]
        [Route("RetrieveById")]
        public async Task<IActionResult> RetrieveById(int id)
        {
            var productoManager = new ProductoManager();

            var producto = productoManager.RetrieveById(id);

            return Ok(producto);
        }

        [HttpGet]
        [Route("RetrieveAll")]
        public async Task<IActionResult> RetrieveByAll()
        {
            var productoManager = new ProductoManager();

            List<Producto> productos = productoManager.RetrieveAll();

            return Ok(productos);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Producto producto)
        {
            var productoManager = new ProductoManager();

            productoManager.Create(producto);

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Producto producto)
        {
            var productoManager = new ProductoManager();

            productoManager.Update(producto);

            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var productoManager = new ProductoManager();

            productoManager.Delete(id);

            return Ok();
        }
    }
}
