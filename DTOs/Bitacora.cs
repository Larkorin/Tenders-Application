namespace DTOs
{
    public class Bitacora : BaseEntity
    {
        public string Descripcion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int UsuarioId { get; set; }
    }
}