using DTOs;
using NPOI.SS.Formula.Functions;
using SistemaProveeduriaDataAcccess.CRUD;

namespace SistemaProveeduriaCORE
{
    public class BitacoraManager
    {
        private BitacoraCrudFactory _bitacoraDataAccessLayer = new();

        public void Create(Bitacora bitacora)
        {
            _bitacoraDataAccessLayer.Create(bitacora);
        }

        public List<Bitacora> RetrieveBitacorasByUserId(int userId)
        {
            List<Bitacora> lstBitacoras = _bitacoraDataAccessLayer.RetrieveBitacorasByUserId<Bitacora>(userId);

            return lstBitacoras;
        }
    }
}
