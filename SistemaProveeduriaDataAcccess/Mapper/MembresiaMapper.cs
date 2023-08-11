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
    public class MembresiaMapper : ISqlStatements, IObjectMapper
    {
        ImpuestoCrudFactory _taxCrudFactory = new ImpuestoCrudFactory();
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var membership = new Membresia
            {
                Id = GetIntValue(row, "ID_MEMBRESIA"),
                Impuestos = null,
                Nombre = GetStringValue(row, "NOMBRE"),
                Precio = GetDoubleValue(row, "PRECIO")
            };

            return membership;
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
            var membershipSql = new SqlOperations { ProcedureName = "CRE_SUSCRIPCION_PR" };

            var memberShip = (Membresia)entity;

            membershipSql.AddVarcharParam("P_NOMBRE", memberShip.Nombre);
            membershipSql.AddDoubleParam("P_PRECIO", memberShip.Precio);


            return membershipSql;
        }

        public SqlOperations GetDeleteStatement(BaseEntity entity)
        {
            var membershipSql = new SqlOperations { ProcedureName = "DEL_MEMBRESIA_PR" };

            var memberShip = (Membresia)entity;

            membershipSql.AddIntParam("ID_MEMBRESIA", memberShip.Id);

            return membershipSql;
        }

        public SqlOperations GetRetriveAllStatement()
        {
            var membershipSql = new SqlOperations { ProcedureName = "RET_ALL_MEMBRESIA_PR" };

            return membershipSql;
        }

        public SqlOperations GetRetriveStatement(int id)
        {
            var membershipSql = new SqlOperations { ProcedureName = "RET_MEMBRESIA_BY_ID_PR" };
            membershipSql.AddIntParam("P_ID_MEMBRESIA", id);

            return membershipSql;
        }

        public SqlOperations GetUpdateStatement(BaseEntity entity)
        {
            var membershipSql = new SqlOperations { ProcedureName = "UPDATE_MEMBRESIA_PR" };
            var memberShip = (Membresia)(entity);

            membershipSql.AddIntParam("P_ID_MEMBRESIA", memberShip.Id);
            membershipSql.AddVarcharParam("P_NOMBRE", memberShip.Nombre);
            membershipSql.AddDoubleParam("P_PRECIO", memberShip.Precio);

            return membershipSql;
        }
        public SqlOperations GetCreateMembresiaImpuestoStatement(int idImpuesto, int idMembresia)
        {
            var sqlMembresia = new SqlOperations { ProcedureName = "CRE_IMPUESTO_MEMBRESIA_PR" };

            sqlMembresia.AddIntParam("P_ID_IMPUESTO", idImpuesto);
            sqlMembresia.AddIntParam("P_ID_MEMBRESIA", idMembresia);

            return sqlMembresia;
        }
        public SqlOperations GetRetrieveAllMembresiaImpuestoStatement()
        {
            var sqlMembresia = new SqlOperations { ProcedureName = "RET_ALL_IMPUESTO_MEMBRESIA_PR" };

            return sqlMembresia;
        }
        public SqlOperations GetUpdateMembresiaImpuestoStatement(int idImpuesto, int idMembresia)
        {
            var sqlMembresia = new SqlOperations { ProcedureName = "UPD_IMPUESTO_MEMBRESIA_PR" };
            sqlMembresia.AddIntParam("P_ID_MEMBRESIA", idMembresia);
            sqlMembresia.AddIntParam("P_ID_IMPUESTO", idImpuesto);

            return sqlMembresia;
        }
        
        public SqlOperations GetTaxesAlreadyAssignStatement(int idTax, int idMembership)
        {
            var sqlUsuario = new SqlOperations { ProcedureName = "RET_IMPUESTO_MEMBRESIA_BY_ID_PR" };

            sqlUsuario.AddIntParam("P_ID_IMPUESTO", idTax);
            sqlUsuario.AddIntParam("P_ID_MEMBRESIA", idMembership);

            return sqlUsuario;
        }
        public SqlOperations GetCreateWithReturnStatement(BaseEntity entity)
        {
            var sqlMembership = new SqlOperations { ProcedureName = "CRE_MEMBRESIA_WITH_RETURN_PR" };
            var newMembership = (Membresia)entity;

            sqlMembership.AddVarcharParam("P_NOMBRE", newMembership.Nombre);
            sqlMembership.AddDoubleParam("P_PRECIO", newMembership.Precio);

            return sqlMembership;
        }
        public String BuildName(Dictionary<string, object> row)
        {
            String name = GetStringValue(row, "NOMBRE");

            return name;
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
