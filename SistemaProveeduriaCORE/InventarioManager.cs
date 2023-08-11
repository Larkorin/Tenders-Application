using DTOs;
using DTOs.ProyectoDTOs;
using SistemaProveeduriaDataAcccess.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaProveeduriaCORE
{
    public class InventarioManager
    {
        private InventarioCrudFactory _inventoryCrudFactory = new InventarioCrudFactory();
        public void Create(Inventario inventory) //, List<Producto> lstProducts
        {
            _inventoryCrudFactory.Create(inventory);

            //foreach (Producto newProduct in lstProducts)
            //{
            //    _inventoryCrudFactory.AssignProducts(newInventory.Id, newProduct.Id, newProduct.Cantidad, newProduct.Precio);
            //}
        }
        public void CreateWithId(int id) //, List<Producto> lstProducts
        {
            _inventoryCrudFactory.CreateWithId(id);

        }
        public Inventario RetrieveById(int id)
        {
            if (_inventoryCrudFactory.Retrieve<Inventario>(id) == default)
                throw new Exception("Error:Inventory was not found");

            return _inventoryCrudFactory.Retrieve<Inventario>(id);
        }
        public Inventario RetrieveByUserId(int id)
        {
            return _inventoryCrudFactory.RetrieveInventarioByUser<Inventario>(id);
        }
        public List<Inventario> RetrieveAll()
        {
            return _inventoryCrudFactory.RetrieveAll<Inventario>();
        }
        public void Update(Inventario inventory)
        {
            if (_inventoryCrudFactory.Retrieve<Inventario>(inventory.Id) == default)
                throw new Exception("Error:Inventory was not found");

            _inventoryCrudFactory.Update(inventory);
        }
        public void Delete(Inventario inventory)
        {
            if (_inventoryCrudFactory.Retrieve<Inventario>(inventory.Id) == default)
                throw new Exception("Error:Inventory was not found");

            _inventoryCrudFactory.Delete(inventory);
        }
        public List<Producto> RetrieveAllInventarioProducts(int inventoryId)
        {
            List<Producto> lstProducts = _inventoryCrudFactory.GetAllProductsByInventario<Producto>(inventoryId);

            return lstProducts;
        }
        public void CreateInventarioProduct(int idInventory, int idProduct, int quantity, double price)
        {
            DateTime date = DateTime.Now;
            _inventoryCrudFactory.AssignProducts(idInventory, idProduct, quantity, price, date);
        }
        public void UpdateProductInventario(int idInventory, int idProduct, int quantity, double price)
        {
            if (_inventoryCrudFactory.Retrieve<Inventario>(idInventory) == default)
                throw new Exception("Error:Inventory was not found");

            _inventoryCrudFactory.UpdateInventarioProductoStatement(idInventory, idProduct, quantity, price);
        }
    }
}
