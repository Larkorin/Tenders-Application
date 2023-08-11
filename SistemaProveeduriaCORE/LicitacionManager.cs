using DTOs;
using SistemaProveeduriaDataAcccess.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaProveeduriaCORE
{
    public class LicitacionManager
    {
        private LicitacionCrudFactory _licitacionCrudFactory = new LicitacionCrudFactory();

        public void Create(Licitacion licitacion)
        {
            Licitacion newlicitacion = _licitacionCrudFactory.CreateWithReturn(licitacion);

            foreach (Producto producto in licitacion.Producto)
            {
                _licitacionCrudFactory.AddProducts(newlicitacion.Id, producto.Id, producto.Cantidad);
            }
        }

        public Licitacion RetrieveById(int id)
        {
            Licitacion licitacion = _licitacionCrudFactory.Retrieve<Licitacion>(id);
            if (licitacion == default)
            {
                throw new Exception("La licitacion no existe");
            }
            return licitacion;
        }
        public List<Licitacion> RetrieveAll()
        {
            List<Licitacion> lstLicitacion = _licitacionCrudFactory.RetrieveAll<Licitacion>();

            return lstLicitacion;
        }
        
        public List<Producto> RetrieveAllLicitacionProducts(int licitacionId)
        {
            List<Producto> lstProducts = _licitacionCrudFactory.GetAllProductsByLicitacion<Producto>(licitacionId);

            return lstProducts;
        }

        public void Update(BaseEntity dto)
        {
            var licitacion = (Licitacion)dto;
            Licitacion licitacionToUse = _licitacionCrudFactory.Retrieve<Licitacion>(licitacion.Id);
            
            if (licitacionToUse == default)
            {
                throw new Exception("La licitacion no existe");
            }

            _licitacionCrudFactory.Update(dto);

            foreach (Producto producto in licitacion.Producto)
            {
                _licitacionCrudFactory.UpdateLicitacionProducto(licitacion.Id, producto.Id, producto.Cantidad);
            }

        }

        public void Delete(BaseEntity dto)
        {
            var licitacion = (Licitacion)dto;
            if (RetrieveById(licitacion.Id) == default)
            {
                throw new Exception("La licitacion no existe");
            }
            else
            {
                _licitacionCrudFactory.Delete(licitacion);
            }
        }

        public void AdjudicarLicitacionOferta(int licitacionId, int ofertaId)
        {
            _licitacionCrudFactory.AdjudicarLicitacionOferta(licitacionId, ofertaId);
        }
    }
}
