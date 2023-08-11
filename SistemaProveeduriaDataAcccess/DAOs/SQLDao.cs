using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaProveeduriaDataAcccess.DAOs
{
    public class SqlDao
    {
        private string _connectionString = "";

        private static SqlDao _instance;

        private SqlDao()
        {

            _connectionString = "Data Source=srv-db00-finalproject2023.database.windows.net;Initial Catalog=fcastror-tendersproject;Persist Security Info=True;User ID=sysman;Password=7EfH!8&R8b*";
        }


        public static SqlDao GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SqlDao();
            }

            return _instance;
        }

        public void ExecuteProcedure(SqlOperations sqlOperation)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                foreach (var param in sqlOperation.Parameters)
                {
                    command.Parameters.Add(param);
                }
                conn.Open();
                command.ExecuteNonQuery();
            }
        }


        //Metodo que funciona para consultar informacion 
        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperations sqlOperation)
        {
            var lsrResult = new List<Dictionary<string, object>>();

            using (var conn = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                foreach (var param in sqlOperation.Parameters)
                {
                    command.Parameters.Add(param);
                }
                conn.Open();
                //De aqui en adelante, trabaja en extraer la data de la consulta
                var reader = command.ExecuteReader();
                //Verifica que tenga filas o resultados de la consulta realizada
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Construimos un diccionario segun cada una de las filas
                        //retornadas por la consulta
                        var dictRow = new Dictionary<string, object>();

                        for (var index = 0; index < reader.FieldCount; index++)
                        {
                            //Agregamos cada una de las columnas al diccionario
                            //como una llave y un valor
                            var key = reader.GetName(index);
                            var value = reader.GetValue(index);
                            dictRow.Add(key, value);
                        }
                        //Guardamos en la lista, el diccionario que representa la fila de la consulta
                        lsrResult.Add(dictRow);
                    }
                }

            }

            return lsrResult;
        }
    }
}
