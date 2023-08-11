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
    public class SuscripcionMapper : ISqlStatements, IObjectMapper
    {
        UsuarioCrudFactory _userCrudFactory = new UsuarioCrudFactory();
        MembresiaCrudFactory _memberShipCrudFactory = new MembresiaCrudFactory();
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var subs = new Suscripcion
            {
                Id = GetIntValue(row, "ID_SUSCRIPCION"),
                UsuarioSuscrito = _userCrudFactory.Retrieve<Usuario>(GetIntValue(row, "ID_USUARIO")),
                Membresia = _memberShipCrudFactory.Retrieve<Membresia>(GetIntValue(row, "ID_MEMBRESIA")),
                FechaSuscripcion = GetDateTimeValue(row, "FECHA_SUSCRIPCION"),
                CorreoPayPal = GetStringValue(row, "CORREO_PAYPAL"),
                Estado = GetStringValue(row, "ESTADO")
            };

            return subs;
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
            var subSql = new SqlOperations { ProcedureName = "CRE_SUSCRIPCION_PR" };

            var subscription = (Suscripcion)entity;

            subSql.AddIntParam("P_ID_USUARIO", subscription.UsuarioSuscrito.Id);
            subSql.AddIntParam("P_ID_MEMBRESIA", subscription.Membresia.Id);
            subSql.AddDateTimeParam("P_FECHA_SUSCRIPCION", subscription.FechaSuscripcion);
            subSql.AddVarcharParam("P_CORREO_PAYPAL", subscription.CorreoPayPal);
            subSql.AddVarcharParam("P_ESTADO", subscription.Estado);


            return subSql;
        }

        public SqlOperations GetDeleteStatement(BaseEntity entity)
        {
            var subsSql = new SqlOperations { ProcedureName = "DEL_SUSCRIPCION_PR" };

            var subscription = (Suscripcion)entity;

            subsSql.AddIntParam("ID_SUSCRIPCION", subscription.Id);

            return subsSql;
        }

        public SqlOperations GetRetriveAllStatement()
        {
            var subsSql = new SqlOperations { ProcedureName = "RET_ALL_USERS_PR" };

            return subsSql;
        }

        public SqlOperations GetRetriveStatement(int id)
        {
            var subsSql = new SqlOperations { ProcedureName = "RET_SUSCRIPCION_BY_ID_PR" };
            subsSql.AddIntParam("P_ID_SUSCRIPCION", id);

            return subsSql;
        }

        public SqlOperations GetUpdateStatement(BaseEntity entity)
        {
            var subsSql = new SqlOperations { ProcedureName = "UPDATE_SUSCRIPCION_PR" };
            var subscription = (Suscripcion)(entity);
            
            subsSql.AddIntParam("P_ID_SUSCRIPCION", subscription.Id);
            subsSql.AddIntParam("P_ID_USUARIO", subscription.UsuarioSuscrito.Id);
            subsSql.AddIntParam("P_ID_MEMBRESIA", subscription.Membresia.Id);
            subsSql.AddDateTimeParam("P_FECHA_SUSCRIPCION", subscription.FechaSuscripcion);
            subsSql.AddVarcharParam("P_CORREO_PAYPAL", subscription.CorreoPayPal);
            subsSql.AddVarcharParam("P_ESTADO", subscription.Estado);

            return subsSql;
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
        /*
         protected Usuario GetUserValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && val is Usuario)
            {
                return (Usuario)dic[attName];
            }
            return null;
        }
        protected Membresia GetMembershipValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && val is Membresia)
            {
                return (Membresia)dic[attName];
            }
            return null;
        }
        */
        protected DateTime GetDateTimeValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && val is DateTime)
            {
                return (DateTime)dic[attName];
            }
            return DateTime.MinValue;
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
