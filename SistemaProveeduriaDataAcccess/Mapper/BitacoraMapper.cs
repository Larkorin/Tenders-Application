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
    public class BitacoraMapper : ISqlStatements, IObjectMapper
    {

        #region "Object Mapper"
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var bitacora = new Bitacora
            {
                Id = GetIntValue(row, "ID_BITACORA"),
                Descripcion = GetStringValue(row, "DESCRIPCION"),
                FechaRegistro = GetDateTimeValue(row, "FECHA_REGISTRO"),
                UsuarioId = GetIntValue(row, "ID_USUARIO")
            };

            return bitacora;
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
            var sqlContrasena = new SqlOperations { ProcedureName = "CRE_BITACORA_PR" };
            var newBitacora = (Bitacora)entity;

            sqlContrasena.AddIntParam("P_ID_USUARIO", newBitacora.UsuarioId);
            sqlContrasena.AddVarcharParam("P_DESCRIPCION", newBitacora.Descripcion);
            sqlContrasena.AddDateTimeParam("P_FECHA_REGISTRO", newBitacora.FechaRegistro);

            return sqlContrasena;
        }

        public SqlOperations GetRetriveByUserIdStatement(int userId)
        {
            var sqlContrasena = new SqlOperations { ProcedureName = "RET_BITACORA_BY_USER_ID_PR" };

            sqlContrasena.AddIntParam("P_ID_USUARIO", userId);

            return sqlContrasena;
        }

        public SqlOperations GetRetriveStatement(int id)
        {
            throw new NotImplementedException();
        }

        public SqlOperations GetRetriveAllStatement()
        {
            throw new NotImplementedException();
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
