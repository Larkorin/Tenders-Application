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
    public class DocumentoMapper : ISqlStatements, IObjectMapper

    {
        UsuarioCrudFactory _userCrudFactory = new UsuarioCrudFactory();
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            byte[] archivoByte = GetByteValue(row, "DOCUMENTO");
            string archivoString = Encoding.UTF8.GetString(archivoByte);
            
            var documento = new Documento
            {
                Usuario = _userCrudFactory.Retrieve<Usuario>(GetIntValue(row, "ID_USUARIO")),
                Archivo = archivoString
            }; 
            return documento;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var documento = BuildObject(row);
                lstResults.Add(documento);
            }
            return lstResults;
        }

        public SqlOperations GetCreateStatement(BaseEntity entity)
        {
            var sqlDocumento = new SqlOperations { ProcedureName = "CRE_DOCUMENTO_PR" };
            var documento = (Documento)entity;
            byte[] archivoBytes = Encoding.UTF8.GetBytes(documento.Archivo);

            sqlDocumento.AddIntParam("P_ID_USUARIO", documento.Usuario.Id);
            sqlDocumento.AddByteParam("P_DOCUMENTO", archivoBytes);

            return sqlDocumento;
        }

        public SqlOperations GetDeleteStatement(BaseEntity entity)
        {
            var sqlDocumento = new SqlOperations { ProcedureName = "DEL_DOCUMENTO_PR" };

            var documento = (Documento)entity;

            sqlDocumento.AddIntParam("P_ID_DOCUMENTO", documento.Id);

            return sqlDocumento;
        }

        public SqlOperations GetRetriveAllStatement()
        {
            var sqlDocumento = new SqlOperations { ProcedureName = "RET_ALL_DOCUMENTO_PR" };
            return sqlDocumento;
        }
        public SqlOperations GetRetriveStatement(int id)
        {
            var sqlDocumento = new SqlOperations { ProcedureName = "RET_DOCUMENTO_BY_ID_PR" };
            sqlDocumento.AddIntParam("P_ID_DOCUMENTO", id);

            return sqlDocumento;
        }

        public SqlOperations GetUpdateStatement(BaseEntity entity)
        {
            var sqlDocumento = new SqlOperations { ProcedureName = "UPDATE_DOCUMENTO_PR" };

			var documento = (Documento)entity;
            byte[] archivoBytes = Encoding.UTF8.GetBytes(documento.Archivo);
            sqlDocumento.AddIntParam("P_ID_DOCUMENTO", documento.Id);
            sqlDocumento.AddIntParam("P_ID_USUARIO", documento.Usuario.Id);
            sqlDocumento.AddByteParam("P_DOCUMENTO", archivoBytes);

			return sqlDocumento;
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

        protected byte[] GetByteValue(Dictionary<string, object> dic, string attName)
        {
            if (dic.ContainsKey(attName) && dic[attName] is byte[])
                return (byte[])dic[attName];

            return null;
        }


    }
}
