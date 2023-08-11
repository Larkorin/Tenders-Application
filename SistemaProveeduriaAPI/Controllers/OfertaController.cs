using DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaProveeduriaCORE;

namespace SistemaProveeduriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfertaController : ControllerBase
    {
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Oferta oferta)
        {
            var ofertaManager = new OfertaManager();
            ofertaManager.Create(oferta);
            return Ok();
        }

        [HttpGet]
        [Route("RetrieveById")]
        public async Task<IActionResult> RetrieveById(int id)
        {
            var ofertaManager = new OfertaManager();
            var oferta = ofertaManager.RetrieveById(id);
            return Ok(oferta);
        }

        [HttpGet]
        [Route("RetrieveAll")]
        public async Task<IActionResult> RetrieveAll()
        {
            var ofertaManager = new OfertaManager();
            List<Oferta> ofertas = ofertaManager.RetrieveAll();
            return Ok(ofertas);
        }
        
        [HttpGet]
        [Route("RetrieveAllOffersByTenderId")]
        public async Task<IActionResult> RetrieveAllOffersByTenderId(int tenderId)
        {
            var ofertaManager = new OfertaManager();
            List<Oferta> ofertas = ofertaManager.RetrieveAllOffersByTenderId(tenderId);
            return Ok(ofertas);
        }
        
        [HttpGet]
        [Route("RetriveOffersByUserId")]
        public async Task<IActionResult> RetriveOffersByUserId(int userId)
        {
            var ofertaManager = new OfertaManager();
            List<Oferta> ofertas = ofertaManager.RetriveOfferByUserId(userId);
            return Ok(ofertas);
        }
        
        [HttpGet]
        [Route("RetriveProductsByOfferId")]
        public async Task<IActionResult> RetriveProductsByOfferId(int offerId)
        {
            var ofertaManager = new OfertaManager();
            List<Producto> productos = ofertaManager.RetriveProductsByOfferId(offerId);
            return Ok(productos);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Oferta oferta)
        {
            var ofertaManager = new OfertaManager();
            ofertaManager.Update(oferta);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Oferta oferta)
        {
            var ofertaManager = new OfertaManager();
            ofertaManager.Delete(oferta);
            return Ok();
        }
    }
}
