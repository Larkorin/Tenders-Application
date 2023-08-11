using System.Data;
using System.Data.SqlClient;

namespace SistemaProveeduriaDataAcccess.DAOs
{
    public class SqlOperations
    {
        public string ProcedureName { get; set; }
        public List<SqlParameter> Parameters { get; set; }

        public SqlOperations()
        {
            Parameters = new List<SqlParameter>();
        }

        public void AddVarcharParam(string paramName, string paramValue)
        {
            Parameters.Add(new SqlParameter(paramName, paramValue));
        }

        public void AddIntParam(string paramName, int paramValue)
        {
            Parameters.Add(new SqlParameter(paramName, paramValue));
        }
        public void AddDateTimeParam(string paramName, DateTime paramValue)
        {
            Parameters.Add(new SqlParameter(paramName, paramValue));
        }
        public void AddBooleanParam(string paramName, bool paramValue)
        {
            Parameters.Add(new SqlParameter(paramName, paramValue));
        }
        public void AddDoubleParam(string paramName, double paramValue)
        {
            Parameters.Add(new SqlParameter(paramName, paramValue));
        }
        public void AddByteParam(string paramName, byte[] paramValue)
        {
            Parameters.Add(new SqlParameter(paramName, SqlDbType.VarBinary, -1) { Value = paramValue });
        }
    }
}