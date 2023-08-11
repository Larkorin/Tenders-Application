using DTOs;
using SistemaProveeduriaDataAcccess.CRUD;

namespace SistemaProveeduriaCORE
{
    public class PermisoManager
    {
        private PermisoCrudFactory _permisoDataAccessLayer = new();

        public void Create(Permiso permiso)
        {
            _permisoDataAccessLayer.Create(permiso);
        }

        public Permiso RetrieveById(int id)
        {

            Permiso permiso = _permisoDataAccessLayer.Retrieve<Permiso>(id);

            if (permiso == null)
            {
                throw new ArgumentException("El permiso no existe");
            }

            return permiso;
        }

        public List<Permiso> RetrieveAll()
        {

            List<Permiso> permisos = _permisoDataAccessLayer.RetrieveAll<Permiso>();

            return permisos;
        }

        public void Update(Permiso permiso)
        {

            Permiso existingPermiso = _permisoDataAccessLayer.Retrieve<Permiso>(permiso.Id);

            if (existingPermiso == null)
            {
                throw new ArgumentException("El permiso no existe");
            }

            _permisoDataAccessLayer.Update(permiso);
        }

        public void Delete(int id)
        {

            Permiso permiso = _permisoDataAccessLayer.Retrieve<Permiso>(id);

            if (permiso == null)
            {
                throw new ArgumentException("El permiso no existe");
            }

            _permisoDataAccessLayer.Delete(permiso);
        }
    }
}
