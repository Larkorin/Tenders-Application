using DTOs;
using Microsoft.AspNetCore.Mvc;
using SistemaProveeduriaCORE;

namespace SistemaProveeduriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Documento documento)
        {
            var documentoManager = new DocumentoManager();
            documentoManager.Create(documento);
            return Ok();
        }

        [HttpGet]
        [Route("RetrieveById")]
        public async Task<IActionResult> RetrieveById(int id)
        {
            var documentoManager = new DocumentoManager();
            var documento = documentoManager.RetrieveById(id);
            return Ok(documento);
        }

        [HttpGet]
        [Route("RetrieveAll")]
        public async Task<IActionResult> RetrieveAll()
        {
            var documentoManager = new DocumentoManager();
            List<Oferta> docoumento = documentoManager.RetrieveAll();
            return Ok(docoumento);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Documento documento)
        {
            var documentoManager = new DocumentoManager();
            documentoManager.Update(documento);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Documento documento)
        {
            var documentoManager = new DocumentoManager();
            documentoManager.Delete(documento);
            return Ok();
        }
    }
}
