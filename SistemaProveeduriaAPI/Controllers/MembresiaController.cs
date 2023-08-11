using DTOs;
using DTOs.ProyectoDTOs;
using Microsoft.AspNetCore.Mvc;
using SistemaProveeduriaCORE;

namespace SistemaProveeduriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembresiaController : Controller
    {
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Membresia memberShip)
        {
            var um = new MembresiaManager();

            um.Create(memberShip);

            return Ok();
        }
        [HttpGet]
        [Route("RetrieveById")]
        public async Task<IActionResult> RetrieveById(int id)
        {
            var um = new MembresiaManager();

            var memberShipSelected = um.RetrieveById(id);

            return Ok(memberShipSelected);
        }
        [HttpGet]
        [Route("RetrieveAll")]
        public async Task<IActionResult> RetrieveAll()
        {
            var um = new MembresiaManager();

            List<Membresia> memberShipList = um.RetrieveAll();

            return Ok(memberShipList);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Membresia memberShip)
        {
            var um = new MembresiaManager();

            um.Update(memberShip);

            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Membresia memberShip)
        {
            var um = new MembresiaManager();

            um.Delete(memberShip);

            return Ok();
        }
    }
}
