using System.Collections;

namespace DTOs.ProyectoDTOs
{
    public class Inventario : BaseEntity
    {
        public int Usuario { get; set; }
        public List<Producto> Productos { get; set; }        
    }
}
