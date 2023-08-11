using DTOs.ProyectoDTOs;

namespace DTOs
{
    public class Producto : BaseEntity
    {
        public string Nombre { get; set; }
        public string UnidadMedida { get; set; }
        public int Cantidad { get; set; }
        public List<int> Impuestos { get; set; }
        public double Precio { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
