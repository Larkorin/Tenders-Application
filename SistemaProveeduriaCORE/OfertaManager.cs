using DTOs;
using SistemaProveeduriaDataAcccess.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaProveeduriaCORE
{
    public class OfertaManager
    {
        private OfertaCrudFactory _ofertaCrudFactory = new OfertaCrudFactory();

        public void Create(Oferta oferta)
        {
            Oferta newOffer = _ofertaCrudFactory.CreateWithReturn(oferta);
            
            foreach (Producto producto in oferta.Producto)
            {
                _ofertaCrudFactory.AddProducts(newOffer.Id, producto.Id, producto.Cantidad);
            }
        }
        public Oferta RetrieveById(int id)
        {
            Oferta oferta = _ofertaCrudFactory.Retrieve<Oferta>(id);
            if (oferta == default)
            {
                throw new Exception("La oferta no existe");
            }
            return oferta;
        }
        public List<Oferta> RetrieveAll()
        {
            List<Oferta> lstOfertas = _ofertaCrudFactory.RetrieveAll<Oferta>();

            return lstOfertas;
        }
        
        public List<Oferta> RetrieveAllOffersByTenderId(int tenderId)
        {
            List<Oferta> lstOfertas = _ofertaCrudFactory.RetrieveAllOffersByTenderId<Oferta>(tenderId);

            return lstOfertas;
        }
        
        public List<Oferta> RetriveOfferByUserId(int userId)
        {
            List<Oferta> lstOfertas = _ofertaCrudFactory.RetriveOfferByUserId<Oferta>(userId);

            return lstOfertas;
        }

        public List<Producto> RetriveProductsByOfferId(int offerId)
        {
            List<Producto> lstProducts = _ofertaCrudFactory.RetriveProductsByOfferId<Producto>(offerId);

            return lstProducts;
        }

        public void Update(BaseEntity dto)
        {
            var oferta = (Oferta)dto;
            if (RetrieveById(oferta.Id) == default)
            {
                throw new Exception("La oferta no existe");
            }
            else
            {
                _ofertaCrudFactory.Update(dto);
            }
        }

        public void Delete(BaseEntity dto)
        {
            var oferta = (Oferta)dto;
            if (RetrieveById(oferta.Id) == default)
            {
                throw new Exception("La oferta no existe");
            }
            else
            {
                _ofertaCrudFactory.Delete(oferta);
            }
        }
    }
}
