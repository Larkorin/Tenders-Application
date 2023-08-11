using DTOs;
using NPOI.SS.Formula.Functions;
using SistemaProveeduriaDataAcccess.CRUD;

namespace SistemaProveeduriaCORE
{
    public class FacturaManager
    {
        private FacturaCrudFactory _facturaDataAccessLayer = new();

        public void Create(Factura factura)
        {
            _facturaDataAccessLayer.Create(factura);
        }

        public List<Factura> RetriveByUserInCharge(int userId)
        {
            List<Factura> lstFacturas = _facturaDataAccessLayer.RetriveByUserInCharge<Factura>(userId);

            return lstFacturas;
        }
        
        public List<Factura> RetriveByUserReceiver(int userId)
        {
            List<Factura> lstFacturas = _facturaDataAccessLayer.RetriveByUserReceiver<Factura>(userId);

            return lstFacturas;
        }

        public List<Factura> RetrieveAll()
        {
            List<Factura> lstBills = _facturaDataAccessLayer.RetrieveAll<Factura>();

            return lstBills;
        }
    }
}