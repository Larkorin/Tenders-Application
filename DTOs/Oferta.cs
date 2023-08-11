namespace DTOs
{
    public class Oferta : BaseEntity
    {
        public int Oferente { get; set; }
        public int Licitacion { get; set; }
        public string Estado { get; set; }
        public double Precio { get; set; }
        public List<Producto> Producto { get; set; }
    }
}
