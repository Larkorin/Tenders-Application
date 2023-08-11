using DTOs;
using SistemaProveeduriaDataAcccess.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaProveeduriaDataAcccess.Mapper
{
    internal class ProductoMapper : ISqlStatements, IObjectMapper
    {
        #region "Object Mapper"
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var producto = new Producto
            {
                Id = GetIntValue(row, "ID_PRODUCTO"),
                Nombre = GetStringValue(row, "NOMBRE"),
                UnidadMedida = GetStringValue(row, "UNIDAD_MEDIDA")
            };

            return producto;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var producto = BuildObject(row);
                lstResults.Add(producto);
            }

            return lstResults;
        }
        #endregion

        protected int GetIntValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && (val is int || val is decimal))
                return (int)dic[attName];

            return -1;
        }

        protected string GetStringValue(Dictionary<string, object> dic, string attName)
        {
            if (dic.ContainsKey(attName) && dic[attName] is string)
            {
                return (string)dic[attName];
            }

            return null;
        }

        #region "SQL Statements"
        public SqlOperations GetCreateStatement(BaseEntity entity)
        {
            var sqlProducto = new SqlOperations { ProcedureName = "CRE_PRODUCTO_PR" };
            var newProducto = (Producto)entity;

            sqlProducto.AddVarcharParam("P_NOMBRE", newProducto.Nombre);
            sqlProducto.AddVarcharParam("P_UNIDAD_MEDIDA", newProducto.UnidadMedida);

            return sqlProducto;
        }

        public SqlOperations GetDeleteStatement(BaseEntity entity)
        {
            var sqlProducto = new SqlOperations { ProcedureName = "DEL_PRODUCTO_PR" };
            var productoToDelete = (Producto)entity;

            sqlProducto.AddIntParam("P_ID_PRODUCTO", productoToDelete.Id);

            return sqlProducto;
        }

        public SqlOperations GetRetriveAllStatement()
        {
            var sqlProducto = new SqlOperations { ProcedureName = "RET_ALL_PRODUCTO_PR" };

            return sqlProducto;
        }

        public SqlOperations GetRetriveStatement(int id)
        {
            var sqlProducto = new SqlOperations { ProcedureName = "RET_PRODUCTO_BY_ID_PR" };

            sqlProducto.AddIntParam("P_ID_PRODUCTO", id);

            return sqlProducto;
        }

        public SqlOperations GetUpdateStatement(BaseEntity entity)
        {
            var sqlProducto = new SqlOperations { ProcedureName = "UPDATE_PRODUCTO_PR" };
            var newProducto = (Producto)entity;

            sqlProducto.AddIntParam("P_ID_PRODUCTO", newProducto.Id);
            sqlProducto.AddVarcharParam("P_NOMBRE", newProducto.Nombre);
            sqlProducto.AddVarcharParam("P_UNIDAD_MEDIDA", newProducto.UnidadMedida);

            return sqlProducto;
        }

        public SqlOperations GetCreateWithReturnStatement(BaseEntity entity)
        {
            var sqlProducto = new SqlOperations { ProcedureName = "CRE_PRODUCTO_WITH_RETURN_PR" };
            var newProducto = (Producto)entity;

            sqlProducto.AddVarcharParam("P_NOMBRE", newProducto.Nombre);
            sqlProducto.AddVarcharParam("P_UNIDAD_MEDIDA", newProducto.UnidadMedida);
            sqlProducto.AddIntParam("P_ID_PRODUCTO", 0);

            return sqlProducto;
        }

        public SqlOperations GetAssignTaxesStatement(int taxId, int userId)
        {
            var sqlProducto = new SqlOperations { ProcedureName = "CRE_IMPUESTO_PRODUCTO_PR" };

            sqlProducto.AddIntParam("P_ID_IMPUESTO", taxId);
            sqlProducto.AddIntParam("P_ID_PRODUCTO", userId);

            return sqlProducto;
        }
        #endregion
    }
}
