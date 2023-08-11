using DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaProveeduriaCORE;

namespace SistemaProveeduriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuscripcionController : Controller
    {
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Suscripcion subscription)
        {
            var um = new SuscripcionManager();

            um.Create(subscription);

            return Ok();
        }
        [HttpGet]
        [Route("RetrieveById")]
        public async Task<IActionResult> RetrieveById(int id)
        {
            var um = new SuscripcionManager();

            var subscriptionSelected = um.RetrieveById(id);

            return Ok(subscriptionSelected);
        }
        [HttpGet]
        [Route("RetrieveAll")]
        public async Task<IActionResult> RetrieveAll()
        {
            var um = new SuscripcionManager();

            List<Suscripcion> subscriptionList = um.RetrieveAll();

            return Ok(subscriptionList);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Suscripcion subscription)
        {
            var um = new SuscripcionManager();

            um.Update(subscription);

            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Suscripcion subscription)
        {
            var um = new SuscripcionManager();

            um.Delete(subscription);

            return Ok();
        }
    }
}
