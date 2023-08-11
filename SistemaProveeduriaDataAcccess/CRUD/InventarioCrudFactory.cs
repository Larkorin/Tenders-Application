using DTOs;
using DTOs.ProyectoDTOs;
using SistemaProveeduriaDataAcccess.DAOs;
using SistemaProveeduriaDataAcccess.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaProveeduriaDataAcccess.CRUD
{
    public class InventarioCrudFactory : CrudFactory
    {
        InventarioMapper _mapper;

        public InventarioCrudFactory()
        {
            _mapper = new InventarioMapper();
            dao = SqlDao.GetInstance();
        }
        public override void Create(BaseEntity dto)
        {
            var inventory = (Inventario)dto;
            var inventorySql = _mapper.GetCreateStatement(inventory);
            dao.ExecuteProcedure(inventorySql);
        }
        public void CreateWithId(int id)
        {
            var inventorySql = _mapper.GetCreate2Statement(id);
            dao.ExecuteProcedure(inventorySql);
        }
        public override void Delete(BaseEntity dto)
        {
            var inventory = (Inventario)dto;
            var inventorySql = _mapper.GetDeleteStatement(inventory);
            dao.ExecuteProcedure(inventorySql);
        }

        public override T Retrieve<T>(int id)
        {
            var sqlInventory = _mapper.GetRetriveStatement(id);
            var results = dao.ExecuteQueryProcedure(sqlInventory);

            if (results.Count == 1)
            {
                var objUser = _mapper.BuildObject(results[0]);

                return ((T)Convert.ChangeType(objUser, typeof(T)));

            }
            return default;
        }
        public T RetrieveInventarioByUser<T>(int id)
        {
            var sqlInventory = _mapper.GetRetrieveStatement(id);
            var results = dao.ExecuteQueryProcedure(sqlInventory);

            if (results.Count == 1)
            {
                var objUser = _mapper.BuildObject(results[0]);

                return ((T)Convert.ChangeType(objUser, typeof(T)));

            }
            return default;
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstInventory = new List<T>();

            //Buscamos el statement para hacer un retrieve all
            var sqlInventory = _mapper.GetRetriveAllStatement();

            //Ejecutamos el retrieve all
            var lstResults = dao.ExecuteQueryProcedure(sqlInventory);

            if (lstResults.Count > 0)
            {
                var objsMathOperation = _mapper.BuildObjects(lstResults);

                foreach (var op in objsMathOperation)
                {
                    lstInventory.Add((T)Convert.ChangeType(op, typeof(T)));
                }
            }

            return lstInventory;
        }

        public override void Update(BaseEntity dto)
        {
            var inventory = (Inventario)dto;
            var inventorySql = _mapper.GetUpdateStatement(inventory);
            dao.ExecuteProcedure(inventorySql);
        }

        public T RetrieveInventarioProduct<T>(int idInventory, int idProduct)
        {
            var inventorySql = _mapper.GetProductsAlreadyAssignStatement(idInventory, idProduct);

            var result = dao.ExecuteQueryProcedure(inventorySql);

            if (result.Count == 1)
            {
                var obj = _mapper.BuildObject(result[0]);
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            else
            {
                return default;
            }
        }

        public List<T> GetAllProductsByInventarios<T>()
        {
            var lstInventario = new List<T>();

            var sqlInventario = _mapper.GetRetrieveAllInventarioProductoStatement();

            //Ejecutamos el retrieve all
            var lstResults = dao.ExecuteQueryProcedure(sqlInventario);

            if (lstResults.Count > 0)
            {
                var objsUsers = _mapper.BuildProductObjects(lstResults);

                foreach (var user in objsUsers)
                {
                    lstInventario.Add((T)Convert.ChangeType(user, typeof(T)));
                }
            }

            return lstInventario;
        }
        public List<T> GetAllProductsByInventario<T>(int idInventory)
        {
            var lstInventario = new List<T>();

            var sqlInventario = _mapper.GetProductsInInventory(idInventory);

            //Ejecutamos el retrieve all
            var lstResults = dao.ExecuteQueryProcedure(sqlInventario);

            if (lstResults.Count > 0)
            {
                var objsUsers = _mapper.BuildProductObjects(lstResults);

                foreach (var user in objsUsers)
                {
                    lstInventario.Add((T)Convert.ChangeType(user, typeof(T)));
                }
            }

            return lstInventario;
        }

        public void AssignProducts(int idInventory, int idProduct, int quantity, double price, DateTime date)
        {
            var sqlInventory = _mapper.GetAssignProductStatement(idInventory, idProduct, quantity, price, date);

            dao.ExecuteProcedure(sqlInventory);
        }

        public void UpdateInventarioProductoStatement(int idInventory, int idProduct, int quantity, double price)
        {
            var sqlInventory = _mapper.GetUpdateInventarioProductoStatement(idInventory, idProduct, quantity, price);

            dao.ExecuteProcedure(sqlInventory);
        }
        public Inventario CreateWithReturn(BaseEntity dto)
        {
            var newInventory = (Inventario)dto;

            var sqlInventory = _mapper.GetCreateWithReturnStatement(newInventory);

            var lstResults = dao.ExecuteQueryProcedure(sqlInventory);

            if (lstResults.Count == 1)
            {
                var obj = _mapper.BuildObject(lstResults[0]);

                return (Inventario)Convert.ChangeType(obj, typeof(Inventario));
            }
            else
            {
                return null;
            }
        }
    }
}
