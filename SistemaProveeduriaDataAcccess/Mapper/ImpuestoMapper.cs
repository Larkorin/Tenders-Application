using DTOs;
using DTOs.ProyectoDTOs;
using SistemaProveeduriaDataAcccess.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaProveeduriaDataAcccess.Mapper
{
    public class ImpuestoMapper : ISqlStatements, IObjectMapper
    {
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var tax = new Impuesto
            {
                Id = GetIntValue(row, "ID_IMPUESTO"),
                Nombre = GetStringValue(row, "NOMBRE"),
                ValorPorcentual = GetDoubleValue(row, "VALOR_PORCENTUAL")
            };

            System.Console.WriteLine(tax.ValorPorcentual);

            return tax;
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

        public SqlOperations GetCreateStatement(BaseEntity entity)
        {
            var taxSql = new SqlOperations { ProcedureName = "CRE_IMPUESTO_PR" };

            var tax = (Impuesto)entity;

            taxSql.AddVarcharParam("P_NOMBRE", tax.Nombre);
            taxSql.AddDoubleParam("P_VALOR_PORCENTUAL", tax.ValorPorcentual);


            return taxSql;
        }

        public SqlOperations GetDeleteStatement(BaseEntity entity)
        {
            var taxSql = new SqlOperations { ProcedureName = "DEL_IMPUESTO_PR" };

            var tax = (Impuesto)entity;

            taxSql.AddIntParam("ID_IMPUESTO", tax.Id);

            return taxSql;
        }

        public SqlOperations GetRetriveAllStatement()
        {
            var inventorySql = new SqlOperations { ProcedureName = "RET_ALL_IMPUESTO_PR" };

            return inventorySql;
        }

        public SqlOperations GetRetriveStatement(int id)
        {
            var taxSql = new SqlOperations { ProcedureName = "RET_IMPUESTO_BY_ID_PR" };
            taxSql.AddIntParam("P_ID_IMPUESTO", id);

            return taxSql;
        }

        public SqlOperations GetUpdateStatement(BaseEntity entity)
        {
            var taxSql = new SqlOperations { ProcedureName = "UPDATE_IMPUESTO_PR" };
            var tax = (Impuesto)(entity);

            taxSql.AddIntParam("P_ID_IMPUESTO", tax.Id);
            taxSql.AddVarcharParam("P_NOMBRE", tax.Nombre);
            taxSql.AddDoubleParam("P_VALOR_PORCENTUAL", tax.ValorPorcentual);

            return taxSql;
        }
        public SqlOperations GetRetriveMemberShipTaxStatement(int idMembership)
        {
            var sqlTax = new SqlOperations { ProcedureName = "RET_IMPUESTO_MEMBRESIA_BY_MEMBRESIA_ID_PR" };

            sqlTax.AddIntParam("P_ID_MEMBRESIA", idMembership);

            return sqlTax;
        }

        protected string GetStringValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && val is string)
            {
                return (string)dic[attName];
            }
            return string.Empty;
        }
        protected double GetDoubleValue(Dictionary<string, object> dic, string attName)
        {
            object val;
            if (dic.TryGetValue(attName, out val) && (val is int || val is double || val is decimal))
                return Convert.ToDouble(val);

            return -1.0;
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
