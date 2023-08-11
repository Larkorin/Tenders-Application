using DTOs;
using SistemaProveeduriaDataAcccess.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaProveeduriaDataAcccess.Mapper
{
    public class PermisoMapper : ISqlStatements, IObjectMapper
    {
        #region "Object Mapper"
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var permiso = new Permiso
            {
                Id = GetIntValue(row, "ID_PERMISO"),
                Nombre = GetStringValue(row, "NOMBRE"),
            };

            return permiso;
        }

        public BaseEntity BuildObjectWithStatus(Dictionary<string, object> row)
        {
            var permiso = new Permiso
            {
                Id = GetIntValue(row, "ID_PERMISO"),
                Nombre = GetStringValue(row, "NOMBRE"),
                Estado = GetStringValue(row, "ESTADO"),
            };

            System.Console.WriteLine(permiso.Estado);

            return permiso;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var permiso = BuildObject(row);
                lstResults.Add(permiso);
            }

            return lstResults;
        }

        public List<BaseEntity> BuildObjectsWithStatus(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var permiso = BuildObjectWithStatus(row);
                lstResults.Add(permiso);
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
            var sqlPermiso = new SqlOperations { ProcedureName = "CRE_PERMISO_PR" };
            var newPermiso = (Permiso)entity;

            sqlPermiso.AddVarcharParam("P_NOMBRE", newPermiso.Nombre);

            return sqlPermiso;
        }

        public SqlOperations GetDeleteStatement(BaseEntity entity)
        {
            var sqlPermiso = new SqlOperations { ProcedureName = "DEL_PERMISO_PR" };
            var permisoToDelete = (Permiso)entity;

            sqlPermiso.AddIntParam("P_ID_USUARIO", permisoToDelete.Id);

            return sqlPermiso;
        }

        public SqlOperations GetRetriveAllStatement()
        {
            var sqlPermiso = new SqlOperations { ProcedureName = "RET_ALL_PERMISO_PR" };

            return sqlPermiso;
        }

        public SqlOperations GetRetriveStatement(int id)
        {
            var sqlPermiso = new SqlOperations { ProcedureName = "RET_PERMISO_BY_ID_PR" };
            sqlPermiso.AddIntParam("P_ID_PERMISO", id);

            return sqlPermiso;
        }

        public SqlOperations GetUpdateStatement(BaseEntity entity)
        {
            var sqlPermiso = new SqlOperations { ProcedureName = "UPDATE_PERMISO_PR" };
            var newPermiso = (Permiso)entity;

            sqlPermiso.AddIntParam("P_ID_PERMISO", newPermiso.Id);
            sqlPermiso.AddVarcharParam("P_NOMBRE", newPermiso.Nombre);

            return sqlPermiso;
        }

        public SqlOperations GetRetriveUserPermissionsStatement(int userId)
        {
            var sqlPermiso = new SqlOperations { ProcedureName = "RET_PERMISO_USUARIO_BY_USUARIO_ID_PR" };

            sqlPermiso.AddIntParam("P_ID_USUARIO", userId);

            return sqlPermiso;
        }
        #endregion
    }
}
