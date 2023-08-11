using DTOs;
using SistemaProveeduriaDataAcccess.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaProveeduriaCORE
{
    public class SuscripcionManager
    {
        private SuscripcionCrudFactory _SuscriptionCrudFactory = new SuscripcionCrudFactory();

        public void Create(Suscripcion subscription)
        {
            _SuscriptionCrudFactory.Create(subscription);
        }
        public Suscripcion RetrieveById(int id)
        {
            if (_SuscriptionCrudFactory.Retrieve<Suscripcion>(id) == default)
                throw new Exception("Error:Subscription was not found");

            return _SuscriptionCrudFactory.Retrieve<Suscripcion>(id);
        }
        public List<Suscripcion> RetrieveAll()
        {
            return _SuscriptionCrudFactory.RetrieveAll<Suscripcion>();
        }
        public void Update(Suscripcion subscription)
        {
            if (_SuscriptionCrudFactory.Retrieve<Suscripcion>(subscription.Id) == default)
                throw new Exception("Error:Subscription was not found");

            _SuscriptionCrudFactory.Update(subscription);
        }
        public void Delete(Suscripcion subscription)
        {
            if (_SuscriptionCrudFactory.Retrieve<Suscripcion>(subscription.Id) == default)
                throw new Exception("Error:Subscription was not found");

            _SuscriptionCrudFactory.Delete(subscription);
        }
    }
}
