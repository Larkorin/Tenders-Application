using DTOs;
using SistemaProveeduriaDataAcccess.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaProveeduriaDataAcccess.Mapper
{
    public class EscuelaMapper : ISqlStatements, IObjectMapper
    {
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var escuela = new Escuela
            {
                Id = GetIntValue(row, "ID_ESCUELA"),
                Nombre = GetStringValue(row, "NOMBRE"),
                Ubicacion = GetStringValue(row, "UBICACION")
            };

            return escuela;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var escuela = BuildObject(row);
                lstResults.Add(escuela);
            }
            return lstResults;
        }

        public SqlOperations GetCreateStatement(BaseEntity entity)
        {
            var sqlEscuela = new SqlOperations { ProcedureName = "CRE_ESCUELA_PR" };
            var escuela = (Escuela)entity;

            sqlEscuela.AddVarcharParam("P_NOMBRE", escuela.Nombre);
            sqlEscuela.AddVarcharParam("P_UBICACION", escuela.Ubicacion);

            return sqlEscuela;
        }

        public SqlOperations GetDeleteStatement(BaseEntity entity)
        {
            var sqlEscuela = new SqlOperations { ProcedureName = "DEL_ESCUELA_PR" };
            var escuela = (Escuela)entity;

            sqlEscuela.AddIntParam("P_ID_USUARIO", escuela.Id);

            return sqlEscuela;
        }

        public SqlOperations GetRetriveAllStatement()
        {
            var sqlEscuela = new SqlOperations { ProcedureName = "RET_ALL_ESCUELA_PR" };
            return sqlEscuela;
        }

        public SqlOperations GetRetriveStatement(int id)
        {
            var sqlEscuela = new SqlOperations { ProcedureName = "RET_ESCUELA_BY_ID_PR" };
            sqlEscuela.AddIntParam("P_ID_ESCUELA", id);

            return sqlEscuela;
        }

        public SqlOperations GetUpdateStatement(BaseEntity entity)
        {
            var sqlEscuela = new SqlOperations { ProcedureName = "UPDATE_ESCUELA_PR" };

            var escuela = (Escuela)entity;
            sqlEscuela.AddIntParam("P_ID_ESCUELA", escuela.Id);
            sqlEscuela.AddVarcharParam("P_NOMBRE", escuela.Nombre);
            sqlEscuela.AddVarcharParam("P_UBICACION", escuela.Ubicacion);
            return sqlEscuela;
        }
        protected string GetStringValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && (val is string))
                return (string)dic[attName];

            return null;
        }

        protected int GetIntValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && (val is int || val is decimal))
                return (int)dic[attName];

            return -1;
        }

    }
}
