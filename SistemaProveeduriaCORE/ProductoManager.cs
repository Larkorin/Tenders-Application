using DTOs;
using SistemaProveeduriaDataAcccess.CRUD;

namespace SistemaProveeduriaCORE
{
    public class ProductoManager
    {
        private ProductoCrudFactory _productoDataAccessLayer = new();

        public void Create(Producto producto)
        {
            Producto newProduct = _productoDataAccessLayer.CreateWithReturn(producto);

            foreach (int impuesto in producto.Impuestos)
            {
                _productoDataAccessLayer.AssignTaxes(impuesto, newProduct.Id);
            }
        }

        public Producto RetrieveById(int id)
        {

            Producto producto = _productoDataAccessLayer.Retrieve<Producto>(id);

            if (producto == null)
            {
                throw new ArgumentException("El producto no existe");
            }

            return producto;
        }

        public List<Producto> RetrieveAll()
        {

            List<Producto> productos = _productoDataAccessLayer.RetrieveAll<Producto>();

            return productos;
        }

        public void Update(Producto producto)
        {

            Producto existingProducto = _productoDataAccessLayer.Retrieve<Producto>(producto.Id);

            if (existingProducto == null)
            {
                throw new ArgumentException("El producto no existe");
            }

            _productoDataAccessLayer.Update(producto);
        }

        public void Delete(int id)
        {

            Producto producto = _productoDataAccessLayer.Retrieve<Producto>(id);

            if (producto == null)
            {
                throw new ArgumentException("El producto no existe");
            }

            _productoDataAccessLayer.Delete(producto);
        }
    }
}
