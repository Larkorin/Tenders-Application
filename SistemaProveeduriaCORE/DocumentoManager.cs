using DTOs;
using SistemaProveeduriaDataAcccess.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaProveeduriaCORE
{
    public class DocumentoManager
    {
        private DocumentoCrudFactory _documentoCrudFactory = new DocumentoCrudFactory();

        public void Create(Documento documento)
        {
            _documentoCrudFactory.Create(documento);
        }
        public Documento RetrieveById(int id)
        {
            Documento documento = _documentoCrudFactory.Retrieve<Documento>(id);
            if (documento == default)
            {
                throw new Exception("El documento no existe");
            }
            return documento;
        }
        public List<Oferta> RetrieveAll()
        {
            List<Oferta> lstOfertas = _documentoCrudFactory.RetrieveAll<Oferta>();

            return lstOfertas;
        }

        public void Update(BaseEntity dto)
        {
            var documento = (Documento)dto;
            if (RetrieveById(documento.Id) == default)
            {
                throw new Exception("El documento no existe");
            }
            else
            {
                _documentoCrudFactory.Update(dto);
            }
        }

        public void Delete(BaseEntity dto)
        {
            var documento = (Documento)dto;
            if (RetrieveById(documento.Id) == default)
            {
                throw new Exception("El documento no existe");
            }
            else
            {
                _documentoCrudFactory.Delete(documento);
            }
        }
    }
}
