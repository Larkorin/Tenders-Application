namespace DTOs
{
    public class Documento : BaseEntity
    {
        public Usuario Usuario { get; set; }
        public string Archivo { get; set; }
    }
}
