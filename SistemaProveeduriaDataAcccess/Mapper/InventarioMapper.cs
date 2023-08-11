using DTOs;
using DTOs.ProyectoDTOs;
using SistemaProveeduriaDataAcccess.CRUD;
using SistemaProveeduriaDataAcccess.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaProveeduriaDataAcccess.Mapper
{
    public class InventarioMapper : ISqlStatements, IObjectMapper
    {
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var inventory = new Inventario
            {
                Id = GetIntValue(row, "ID_INVENTARIO"),
                Usuario = GetIntValue(row, "ID_USUARIO"),
                Productos = null
            };

            return inventory;
        }
        public BaseEntity BuildProductObject(Dictionary<string, object> row)
        {
            var producto = new Producto
            {
                Id = GetIntValue(row, "ID_PRODUCTO"),
                UnidadMedida = GetStringValue(row, "UNIDAD_MEDIDA"),
                Nombre = GetStringValue(row, "NOMBRE"),
                Cantidad = GetIntValue(row, "CANTIDAD"),
                Precio = GetDoubleValue(row, "PRECIO"),
                FechaRegistro = GetDateTimeValue(row, "FECHA_REGISTRO")
            };
            return producto;
        }
        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var user = BuildObject(row);
                lstResults.Add(user);
            }
            return lstResults;
        }

        public List<BaseEntity> BuildProductObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var producto = BuildProductObject(row);
                lstResults.Add(producto);
            }
            return lstResults;
        }

        public SqlOperations GetCreateStatement(BaseEntity entity)
        {
            var inventorySql = new SqlOperations { ProcedureName = "CRE_INVENTARIO_PR" };

            var inventory = (Inventario)entity;

            inventorySql.AddIntParam("P_ID_USUARIO", inventory.Usuario);


            return inventorySql;
        }
        public SqlOperations GetCreate2Statement(int id)
        {
            var inventorySql = new SqlOperations { ProcedureName = "CRE_INVENTARIO_PR" };


            inventorySql.AddIntParam("P_ID_USUARIO", id);


            return inventorySql;
        }

        public SqlOperations GetDeleteStatement(BaseEntity entity)
        {
            var inventorySql = new SqlOperations { ProcedureName = "DEL_INVENTARIO_PR" };

            var inventory = (Inventario)entity;

            inventorySql.AddIntParam("ID_INVENTARIO", inventory.Id);

            return inventorySql;
        }

        public SqlOperations GetRetriveAllStatement()
        {
            var inventorySql = new SqlOperations { ProcedureName = "RET_ALL_INVENTARIO_PR" };

            return inventorySql;
        }

        public SqlOperations GetRetriveStatement(int id)
        {
            var inventorySql = new SqlOperations { ProcedureName = "RET_INVENTARIO_BY_ID_PR" };
            inventorySql.AddIntParam("P_ID_INVENTARIO", id);

            return inventorySql;
        }
        public SqlOperations GetRetrieveStatement(int id)
        {
            var inventorySql = new SqlOperations { ProcedureName = "RET_INVENTARIO_BY_USUARIO_ID_PR" };
            inventorySql.AddIntParam("P_ID_USUARIO", id);

            return inventorySql;
        }

        public SqlOperations GetUpdateStatement(BaseEntity entity)
        {
            var inventorySql = new SqlOperations { ProcedureName = "UPDATE_INVENTARIO_PR" };
            var inventory = (Inventario)(entity);

            inventorySql.AddIntParam("P_ID_INVENTARIO", inventory.Id);
            inventorySql.AddIntParam("P_ID_USUARIO", inventory.Usuario);

            return inventorySql;
        }
        public SqlOperations GetAssignProductStatement(int idInventory, int idProduct, int quantity, double price, DateTime date)
        {
            var sqlInventory = new SqlOperations { ProcedureName = "CRE_INVENTARIO_PRODUCTO_PR" };

            sqlInventory.AddIntParam("P_ID_INVENTARIO", idInventory);
            sqlInventory.AddIntParam("P_ID_PRODUCTO", idProduct);
            sqlInventory.AddIntParam("P_CANTIDAD", quantity);
            sqlInventory.AddDoubleParam("P_PRECIO", price);
            sqlInventory.AddDateTimeParam("P_FECHA", date);

            return sqlInventory;
        }
        public SqlOperations GetUpdateInventarioProductoStatement(int idInventory, int idProduct, int quantity, double price)
        {
            var sqlInventory = new SqlOperations { ProcedureName = "UPD_INVENTARIO_PRODUCTO_PR" };

            sqlInventory.AddIntParam("P_ID_INVENTARIO", idInventory);
            sqlInventory.AddIntParam("P_ID_PRODUCTO", idProduct);
            sqlInventory.AddIntParam("P_CANTIDAD", quantity);
            sqlInventory.AddDoubleParam("P_PRECIO", price);
            return sqlInventory;
        }
        public SqlOperations GetRetrieveAllInventarioProductoStatement()
        {
            var sqlInventory = new SqlOperations { ProcedureName = "RET_ALL_INVENTARIO_PRODUCTO_PR" };

            return sqlInventory;
        }
        public SqlOperations GetProductsInInventory(int idInventory)
        {
           //var sqlInventory = new SqlOperations { ProcedureName = "RET_INVENTARIO_PRODUCTO_BY_INVENTARIO_ID_PR" };
            var sqlInventory = new SqlOperations { ProcedureName = "RET_INVENTARIO_PRODUCTO_PR" };

            sqlInventory.AddIntParam("P_ID_INVENTARIO", idInventory);

            return sqlInventory;
        }

        public SqlOperations GetProductsAlreadyAssignStatement(int idInventory, int idProduct)
        {
            var sqlInventory = new SqlOperations { ProcedureName = "RET_INVENTARIO_PRODUCTO_BY_ID_PR" };

            sqlInventory.AddIntParam("P_ID_INVENTARIO", idInventory);
            sqlInventory.AddIntParam("P_ID_PRODUCTO", idProduct);

            return sqlInventory;
        }

        public SqlOperations GetCreateWithReturnStatement(BaseEntity entity)
        {
            var sqlInventory = new SqlOperations { ProcedureName = "CRE_INVENTARIO_WITH_RETURN_PR" };
            var newInventory = (Inventario)entity;

            sqlInventory.AddIntParam("P_ID_USUARIO", newInventory.Usuario);

            return sqlInventory;
        }
        protected int GetIntValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && (val is int || val is decimal))
                return (int)dic[attName];

            return -1;
        }
        protected string GetStringValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && (val is string))
                return (string)dic[attName];

            return null;
        }
        protected DateTime GetDateTimeValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && (val is DateTime))
                return (DateTime)dic[attName];

            return DateTime.MinValue;
        }
        protected double GetDoubleValue(Dictionary<string, object> dic, string attName)
        {
            object val;
            if (dic.TryGetValue(attName, out val) && (val is int || val is double || val is decimal))
                return Convert.ToDouble(val);

            return -1.0;
        }
    }
}
