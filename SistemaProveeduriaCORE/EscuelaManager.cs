using DTOs;
using SistemaProveeduriaDataAcccess.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaProveeduriaCORE
{
    public class EscuelaManager
    {
        private EscuelaCrudFactory _escuelaCrudFactory = new EscuelaCrudFactory();

        public void Create(Escuela escuela)
        {
            _escuelaCrudFactory.Create(escuela);
        }
        public Escuela RetrieveById(int id)
        {
            Escuela escuela = _escuelaCrudFactory.Retrieve<Escuela>(id);
            if (escuela == default)
            {
                throw new Exception("La escuela no existe");
            }
            return escuela;
        }
        public List<Escuela> RetrieveAll()
        {
            List<Escuela> lstEscuelas = _escuelaCrudFactory.RetrieveAll<Escuela>();

            return lstEscuelas;
        }

        public void Update(BaseEntity dto)
        {
            var escuela = (Escuela)dto;

            if (RetrieveById(escuela.Id) == default)
            {
                throw new Exception("La escuela no existe");
            }
            else
            {
                _escuelaCrudFactory.Update(dto);
            }
        }

        public void Delete(BaseEntity dto)
        {
            var escuela = (Escuela)dto;
            if (RetrieveById(escuela.Id) == default)
            {
                throw new Exception("La escuela no existe");
            }
            else
            {
                _escuelaCrudFactory.Delete(escuela);
            }
        }
    }
}
