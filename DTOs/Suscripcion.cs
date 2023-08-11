using DTOs.ProyectoDTOs;

namespace DTOs
{
    public class Suscripcion : BaseEntity
    {
        public Usuario UsuarioSuscrito { get; set; }
        public Membresia Membresia { get; set; }
        public DateTime FechaSuscripcion { get; set; }
        public string CorreoPayPal { get; set; }
        public string Estado { get; set; }
    }
}