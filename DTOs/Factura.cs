namespace DTOs
{
    public class Factura : BaseEntity
    {
        public double Precio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioEmisor { get; set; }
        public int UsuarioReceptor { get; set; }
        public int Licitacion { get; set; }
    }
}