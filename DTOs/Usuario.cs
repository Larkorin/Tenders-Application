namespace DTOs
{
    public class Usuario : BaseEntity
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public List<Permiso> Permisos { get; set; }
        public List<Contrasena> Contrasenas { get; set; }
    }
}
