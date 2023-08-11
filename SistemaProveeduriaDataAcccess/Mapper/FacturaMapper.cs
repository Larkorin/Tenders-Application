using DTOs;
using SistemaProveeduriaDataAcccess.CRUD;
using SistemaProveeduriaDataAcccess.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaProveeduriaDataAcccess.Mapper
{
    public class FacturaMapper : ISqlStatements, IObjectMapper
    {

        #region "Object Mapper"
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var factura = new Factura
            {
                Id = GetIntValue(row, "ID_FACTURA"),
                Precio = GetDoubleValue(row, "PRECIO"),
                FechaCreacion = GetDateTimeValue(row, "FECHA_CREACION"),
                UsuarioEmisor = GetIntValue(row, "ID_USUARIO_EMISOR"),
                UsuarioReceptor = GetIntValue(row, "ID_USUARIO_RECEPTOR"),
                Licitacion = GetIntValue(row, "ID_LICITACION")
            };

            return factura;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var contrasena = BuildObject(row);
                lstResults.Add(contrasena);
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

        protected double GetDoubleValue(Dictionary<string, object> dic, string attName)
        {
            object val;
            if (dic.TryGetValue(attName, out val) && (val is int || val is double || val is decimal))
                return Convert.ToDouble(val);

            return -1.0;
        }

        protected DateTime GetDateTimeValue(Dictionary<string, object> dic, string attName)
        {
            if (dic.ContainsKey(attName) && dic[attName] is DateTime)
            {
                return (DateTime)dic[attName];
            }

            return (DateTime)dic[attName];
        }

        #region "SQL Statements"
        public SqlOperations GetCreateStatement(BaseEntity entity)
        {
            var sqlFactura = new SqlOperations { ProcedureName = "CRE_FACTURA_PR" };
            var newFactura = (Factura)entity;

            sqlFactura.AddDoubleParam("P_PRECIO", newFactura.Precio);
            sqlFactura.AddIntParam("P_ID_USUARIO_EMISOR", newFactura.UsuarioEmisor);
            sqlFactura.AddIntParam("P_ID_LICITACION", newFactura.Licitacion);

            return sqlFactura;
        }

        public SqlOperations GetRetriveByUserInChargeStatement(int userId)
        {
            var sqlFactura = new SqlOperations { ProcedureName = "RET_FACTURAS_BY_USUARIO_EMISOR_PR" };

            sqlFactura.AddIntParam("P_ID_USUARIO_EMISOR", userId);

            return sqlFactura;
        }
        
        public SqlOperations GetRetriveByUserReceiverStatement(int userId)
        {
            var sqlFactura = new SqlOperations { ProcedureName = "RET_FACTURAS_BY_USUARIO_RECEPTOR_PR" };

            sqlFactura.AddIntParam("P_ID_USUARIO_RECEPTOR", userId);

            return sqlFactura;
        }

        public SqlOperations GetRetriveStatement(int id)
        {
            throw new NotImplementedException();
        }

        public SqlOperations GetRetriveAllStatement()
        {
            var sqlFactura = new SqlOperations { ProcedureName = "RET_ALL_FACTURAS_PR" };

            return sqlFactura;
        }

        public SqlOperations GetUpdateStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperations GetDeleteStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
