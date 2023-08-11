using DTOs.ProyectoDTOs;
using Microsoft.AspNetCore.Mvc;
using SistemaProveeduriaCORE;

namespace SistemaProveeduriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImpuestoController : Controller
    {
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Impuesto tax)
        {
            var um = new ImpuestoManager();

            um.Create(tax);
            System.Console.WriteLine(tax.ValorPorcentual);

            return Ok();
        }
        [HttpGet]
        [Route("RetrieveById")]
        public async Task<IActionResult> RetrieveById(int id)
        {
            var um = new ImpuestoManager();

            var taxSelected = um.RetrieveById(id);

            return Ok(taxSelected);
        }
        [HttpGet]
        [Route("RetrieveAll")]
        public async Task<IActionResult> RetrieveAll()
        {
            var um = new ImpuestoManager();

            List<Impuesto> taxList = um.RetrieveAll();

            return Ok(taxList);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Impuesto tax)
        {
            var um = new ImpuestoManager();

            um.Update(tax);

            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Impuesto tax)
        {
            var um = new ImpuestoManager();

            um.Delete(tax);

            return Ok();
        }
    }
}
