namespace DTOs
{
    public class Licitacion : BaseEntity
    {
        public int UsuarioEncargado { get; set; }
        public int UsuarioProveedor { get; set; }
        public int Escuela { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public string CodigoQR { get; set; }
        public List<Producto> Producto { get; set; }
        public double Presupuesto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaCierreOfertas { get; set; }
        public DateTime FechaCierreLicitacion { get; set; }
        public DateTime FechaEntrega { get; set; }
    }
}
