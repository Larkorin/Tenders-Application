using DTOs;
using DTOs.ProyectoDTOs;
using SistemaProveeduriaDataAcccess.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaProveeduriaCORE
{
    public class MembresiaManager
    {
        private MembresiaCrudFactory _memberShipCrudFactory = new MembresiaCrudFactory();
        private ImpuestoCrudFactory _impuestoCrudFactory = new();
        public void Create(Membresia memberShip)
        {
            var newMembership = _memberShipCrudFactory.CreateWithReturn(memberShip);
            
            foreach (int idNewTax in memberShip.Impuestos)
            {
                _memberShipCrudFactory.AddTaxes(idNewTax, newMembership.Id);
            }
        }
        public Membresia RetrieveById(int id)
        {
            if (_memberShipCrudFactory.Retrieve<Membresia>(id) == default)
                throw new Exception("Error:Subscription was not found");

            return _memberShipCrudFactory.Retrieve<Membresia>(id);
        }
        public List<Membresia> RetrieveAll()
        {
            return _memberShipCrudFactory.RetrieveAll<Membresia>();
        }
        public void Update(Membresia memberShip)
        {
            if (_memberShipCrudFactory.Retrieve<Membresia>(memberShip.Id) == default)
                throw new Exception("Error:Subscription was not found");

            _memberShipCrudFactory.Update(memberShip);
        }
        public void Delete(Membresia memberShip)
        {
            if (_memberShipCrudFactory.Retrieve<Membresia>(memberShip.Id) == default)
                throw new Exception("Error:Subscription was not found");

            _memberShipCrudFactory.Delete(memberShip);
        }
        public void UpdateImpuestoMembresia(int idImpuesto ,int idMembresia)
        {
            _memberShipCrudFactory.UpdateImpuestoMembresia(idImpuesto, idMembresia);
        }

        public List<Impuesto> GetMembershipTaxes(int idTax, int idMembership)
        {
            List<Impuesto> taxes = _impuestoCrudFactory.RetrieveMembershipTaxes<Impuesto>(idMembership);

            return taxes;
        }
        

    }

}
