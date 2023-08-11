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
    public class ContrasenaMapper : ISqlStatements, IObjectMapper
    {
        UsuarioCrudFactory _userCrudFactory = new UsuarioCrudFactory();

        #region "Object Mapper"
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var contrasena = new Contrasena
            {
                Id = GetIntValue(row, "ID_CONTRASEÑA"),
                Valor = GetStringValue(row, "CONTRASEÑA"),
                Estado = GetStringValue(row, "ESTADO"),
                FechaActualizacion = GetDateTimeValue(row, "FECHA_ACTUALIZACION"),
                Usuario = _userCrudFactory.Retrieve<Usuario>(GetIntValue(row, "ID_USUARIO"))
            };

            return contrasena;
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
            var sqlContrasena = new SqlOperations { ProcedureName = "CRE_CONTRASENA_PR" };
            var newContrasena = (Contrasena)entity;

            sqlContrasena.AddIntParam("P_ID_USUARIO", newContrasena.Usuario.Id);
            sqlContrasena.AddVarcharParam("P_CONTRASEÑA", newContrasena.Valor);
            sqlContrasena.AddVarcharParam("P_ESTADO", newContrasena.Estado);
            sqlContrasena.AddDateTimeParam("P_FECHA_ACTUALIZACION", newContrasena.FechaActualizacion);
            
            return sqlContrasena;
        }

        public SqlOperations GetDeleteStatement(BaseEntity entity)
        {
            var sqlContrasena = new SqlOperations { ProcedureName = "DEL_CONTRASENA_PR" };
            var contrasenaToDelete = (Usuario)entity;

            sqlContrasena.AddIntParam("P_ID_CONTRASEÑA", contrasenaToDelete.Id);

            return sqlContrasena;
        }

        public SqlOperations GetRetriveAllStatement()
        {
            var sqlContrasena = new SqlOperations { ProcedureName = "RET_ALL_CONTRASENA_PR" };

            return sqlContrasena;
        }

        public SqlOperations GetRetriveStatement(int id)
        {
            var sqlContrasena = new SqlOperations { ProcedureName = "RET_CONTRASENA_BY_ID_PR" };

            sqlContrasena.AddIntParam("P_ID_CONTRASEÑA", id);

            return sqlContrasena;
        }

        public SqlOperations GetUpdateStatement(BaseEntity entity)
        {
            var sqlContrasena = new SqlOperations { ProcedureName = "CRE_CONTRASENA_PR" };
            var newContrasena = (Contrasena)entity;

            sqlContrasena.AddIntParam("P_ID_USUARIO", newContrasena.Usuario.Id);
            sqlContrasena.AddVarcharParam("P_CONTRASEÑA", newContrasena.Valor);
            sqlContrasena.AddVarcharParam("P_ESTADO", newContrasena.Estado);
            sqlContrasena.AddDateTimeParam("P_FECHA_ACTUALIZACION", newContrasena.FechaActualizacion);

            return sqlContrasena;
        }

        public SqlOperations CheckLast5PasswordsStatement(int idUsuario, string password)
        {
            var sqlContrasena = new SqlOperations { ProcedureName = "BUSCAR_CONTRASENAS_PR" };

            sqlContrasena.AddIntParam("P_ID_USUARIO", idUsuario);
            sqlContrasena.AddVarcharParam("P_CONTRASENA_INGRESADA", password);

            return sqlContrasena;
        }

        public SqlOperations SetNewPasswordStatement(int idUsuario, string password)
        {
            var sqlContrasena = new SqlOperations { ProcedureName = "ACTUALIZAR_CONTRASENA_PR" };

            sqlContrasena.AddIntParam("P_ID_USUARIO", idUsuario);
            sqlContrasena.AddVarcharParam("P_NUEVA_CONTRASENA", password);

            return sqlContrasena;
        }

        #endregion
    }
}
