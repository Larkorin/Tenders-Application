using DTOs;
using DTOs.ProyectoDTOs;
using Microsoft.AspNetCore.Mvc;
using SistemaProveeduriaCORE;

namespace SistemaProveeduriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : Controller
    {
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(int id)
        {
            var um = new InventarioManager();

            um.CreateWithId(id);

            return Ok();
        }
        [HttpGet]
        [Route("RetrieveById")]
        public async Task<IActionResult> RetrieveById(int id)
        {
            var um = new InventarioManager();

            var inventorySelected = um.RetrieveById(id);

            return Ok(inventorySelected);
        }

        [HttpGet]
        [Route("RetrieveByUserId")]
        public async Task<IActionResult> RetrieveByUserId(int id)
        {
            var um = new InventarioManager();

            var inventorySelected = um.RetrieveByUserId(id);

            return Ok(inventorySelected);
        }

        [HttpGet]
        [Route("RetrieveAll")]
        public async Task<IActionResult> RetrieveAll()
        {
            var um = new InventarioManager();

            List<Inventario> inventoryList = um.RetrieveAll();

            return Ok(inventoryList);
        }
        [HttpGet]
        [Route("RetrieveAllProductsInventario")]
        public async Task<IActionResult> RetrieveAllProductsInventario(int idInventario)
        {
            var um = new InventarioManager();

            List<Producto> inventoryProductList = um.RetrieveAllInventarioProducts(idInventario);

            return Ok(inventoryProductList);
        }
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Inventario inventory)
        {
            var um = new InventarioManager();

            um.Update(inventory);

            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Inventario inventory)
        {
            var um = new InventarioManager();

            um.Delete(inventory);

            return Ok();
        }
        [HttpPost]
        [Route("AssignProduct")]
        public async Task<IActionResult> AssignProduct(int idInventory, int idProduct, int quantity, double price)
        {
            var um = new InventarioManager();

            um.CreateInventarioProduct(idInventory, idProduct, quantity, price);

            return Ok();
        }
        [HttpPut]
        [Route("UpdateProductInventario")]
        public async Task<IActionResult> UpdateInventarioProduct(int idInventory, int idProduct, int quantity, double price)
        {
            var um = new InventarioManager();

            um.UpdateProductInventario(idInventory, idProduct, quantity, price);

            return Ok();
        }
    }
}
