namespace DTOs
{
    public class Contrasena : BaseEntity
    {
        public string Valor { get; set; }
        public string Estado { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public Usuario Usuario { get; set; }
    }
}
