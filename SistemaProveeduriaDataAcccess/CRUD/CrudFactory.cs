using DTOs;
using SistemaProveeduriaDataAcccess.DAOs;

namespace SistemaProveeduriaDataAcccess.CRUD
{
    public abstract class CrudFactory
    {
        protected SqlDao dao;
        public abstract void Create(BaseEntity dto);
        public abstract T Retrieve<T>(int id);
        public abstract List<T> RetrieveAll<T>();
        public abstract void Update(BaseEntity dto);
        public abstract void Delete(BaseEntity dto);
    }
}