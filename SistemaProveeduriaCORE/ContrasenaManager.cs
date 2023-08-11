using DTOs;
using NPOI.SS.Formula.Functions;
using SistemaProveeduriaDataAcccess.CRUD;

namespace SistemaProveeduriaCORE
{
    public class ContrasenaManager
    {
        private ContrasenaCrudFactory _contrasenaDataAccessLayer = new();

        public void Create(Contrasena contrasena)
        {
            _contrasenaDataAccessLayer.Create(contrasena);
        }

        public Contrasena RetrieveById(int id)
        {

            Contrasena contrasena = _contrasenaDataAccessLayer.Retrieve<Contrasena>(id);

            if (contrasena == null)
            {
                throw new ArgumentException("La contrasena no existe");
            }

            return contrasena;
        }

        public List<Contrasena> RetrieveAll()
        {

            List<Contrasena> contrasenas = _contrasenaDataAccessLayer.RetrieveAll<Contrasena>();

            return contrasenas;
        }

        public void Update(Contrasena contrasena)
        {

            Contrasena existingContrasena = _contrasenaDataAccessLayer.Retrieve<Contrasena>(contrasena.Id);

            if (existingContrasena == null)
            {
                throw new ArgumentException("La contrasena no existe");
            }

            _contrasenaDataAccessLayer.Update(contrasena);
        }

        public void Delete(int id)
        {

            Contrasena contrasena = _contrasenaDataAccessLayer.Retrieve<Contrasena>(id);

            if (contrasena == null)
            {
                throw new ArgumentException("La contrasena no existe");
            }

            _contrasenaDataAccessLayer.Delete(contrasena);
        }

        public bool CheckLast5Passwords(int idUsuario, string password)
        {
            Contrasena contrasena = _contrasenaDataAccessLayer.CheckLast5Passwords<Contrasena>(idUsuario, password);

            if (contrasena == null)
            {
                return true;
            }

            return false;

        }

        public void SetNewPassword(int idUsuario, string password)
        {
            _contrasenaDataAccessLayer.SetNewPassword(idUsuario, password);
        }
    }
}
