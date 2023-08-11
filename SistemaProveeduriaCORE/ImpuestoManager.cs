using DTOs.ProyectoDTOs;
using SistemaProveeduriaDataAcccess.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaProveeduriaCORE
{
    public class ImpuestoManager
    {
        private ImpuestoCrudFactory _taxCrudFactory = new ImpuestoCrudFactory();
        public void Create(Impuesto tax)
        {
            _taxCrudFactory.Create(tax);
        }
        public Impuesto RetrieveById(int id)
        {
            if (_taxCrudFactory.Retrieve<Impuesto>(id) == default)
                throw new Exception("Error:Tax was not found");

            return _taxCrudFactory.Retrieve<Impuesto>(id);
        }
        public List<Impuesto> RetrieveAll()
        {
            return _taxCrudFactory.RetrieveAll<Impuesto>();
        }
        public void Update(Impuesto tax)
        {
            if (_taxCrudFactory.Retrieve<Impuesto>(tax.Id) == default)
                throw new Exception("Error:Tax was not found");

            _taxCrudFactory.Update(tax);
        }
        public void Delete(Impuesto tax)
        {
            if (_taxCrudFactory.Retrieve<Impuesto>(tax.Id) == default)
                throw new Exception("Error:Tax was not found");

            _taxCrudFactory.Delete(tax);
        }
    }
}
