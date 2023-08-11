using DTOs;
using SistemaProveeduriaDataAcccess.DAOs;
using System.Collections ;

namespace SistemaProveeduriaDataAcccess.Mapper
{
    public class UsuarioMapper : ISqlStatements, IObjectMapper
    {
        #region "Object Mapper"
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var usuario = new Usuario
            {
                Id = GetIntValue(row, "ID_USUARIO"),
                Cedula = GetStringValue(row, "DNI"),
                Nombre = GetStringValue(row, "NOMBRE"),
                PrimerApellido = GetStringValue(row, "PRIMER_APELLIDO"),
                SegundoApellido = GetStringValue(row, "SEGUNDO_APELLIDO"),
                Correo = GetStringValue(row, "CORREO"),
                Telefono = GetStringValue(row, "TELEFONO"),
                Estado = GetStringValue(row, "ESTADO"),
                FechaRegistro = GetDateTimeValue(row, "FECHA_REGISTRO")
            };

            return usuario;
        }

        public String BuildPermissionStatus(Dictionary<string, object> row)
        {
            String permissionStatus = GetStringValue(row, "ESTADO");

            return permissionStatus;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var usuario = BuildObject(row);
                lstResults.Add(usuario);
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
            var sqlUsuario = new SqlOperations { ProcedureName = "CRE_USUARIO_PR" };
            var newUsuario = (Usuario)entity;

            sqlUsuario.AddVarcharParam("P_DNI", newUsuario.Cedula);
            sqlUsuario.AddVarcharParam("P_NOMBRE", newUsuario.Nombre);
            sqlUsuario.AddVarcharParam("P_PRIMER_APELLIDO", newUsuario.PrimerApellido);
            sqlUsuario.AddVarcharParam("P_SEGUNDO_APELLIDO", newUsuario.SegundoApellido);
            sqlUsuario.AddVarcharParam("P_CORREO", newUsuario.Correo);
            sqlUsuario.AddVarcharParam("P_TELEFONO", newUsuario.Telefono);
            sqlUsuario.AddVarcharParam("P_ESTADO", newUsuario.Estado);

            return sqlUsuario;
        }

        public SqlOperations GetDeleteStatement(BaseEntity entity)
        {
            var sqlUsuario = new SqlOperations { ProcedureName = "DEL_USUARIO_PR" };
            var usuarioToDelete = (Usuario)entity;

            sqlUsuario.AddIntParam("P_ID_USUARIO", usuarioToDelete.Id);

            return sqlUsuario;
        }

        public SqlOperations GetRetriveAllStatement()
        {
            var sqlUsuario = new SqlOperations { ProcedureName = "RET_ALL_USUARIO_PR" };

            return sqlUsuario;
        }

        public SqlOperations GetRetriveStatement(int id)
        {
            var sqlUsuario = new SqlOperations { ProcedureName = "RET_USUARIO_BY_ID_PR" };

            sqlUsuario.AddIntParam("P_ID_USUARIO", id);

            return sqlUsuario;
        }

        public SqlOperations GetUpdateStatement(BaseEntity entity)
        {
            var sqlUsuario = new SqlOperations { ProcedureName = "UPDATE_USUARIO_PR" };
            var newUsuario = (Usuario)entity;

            sqlUsuario.AddVarcharParam("P_DNI", newUsuario.Cedula);
            sqlUsuario.AddVarcharParam("P_NOMBRE", newUsuario.Nombre);
            sqlUsuario.AddVarcharParam("P_PRIMER_APELLIDO", newUsuario.PrimerApellido);
            sqlUsuario.AddVarcharParam("P_SEGUNDO_APELLIDO", newUsuario.SegundoApellido);
            sqlUsuario.AddVarcharParam("P_CORREO", newUsuario.Correo);
            sqlUsuario.AddVarcharParam("P_TELEFONO", newUsuario.Telefono);
            sqlUsuario.AddVarcharParam("P_ESTADO", newUsuario.Estado);

            return sqlUsuario;
        }

        public SqlOperations GetAssignPermissionsStatement(int permissionId, int userId)
        {
            var sqlUsuario = new SqlOperations { ProcedureName = "CRE_PERMISO_USUARIO_PR" };

            sqlUsuario.AddIntParam("P_ID_PERMISO", permissionId);
            sqlUsuario.AddIntParam("P_ID_USUARIO", userId);
            sqlUsuario.AddVarcharParam("P_ESTADO", "ACTIVO");

            return sqlUsuario;
        }

        public SqlOperations GetPermissionsAlreadyAssignStatement(int permissionId, int userId)
        {
            var sqlUsuario = new SqlOperations { ProcedureName = "RET_PERMISO_USUARIO_BY_ID_PR" };

            System.Console.WriteLine("SP permiso id " + permissionId);

            sqlUsuario.AddIntParam("P_ID_PERMISO", permissionId);
            sqlUsuario.AddIntParam("P_ID_USUARIO", userId);

            return sqlUsuario;
        }

        public SqlOperations GetUpdateUserPermissionsStatement(int permissionId, int userId, String permissionStatus)
        {
            var sqlUsuario = new SqlOperations { ProcedureName = "UPDATE_PERMISO_USUARIO_PR" };

            sqlUsuario.AddIntParam("P_ID_PERMISO", permissionId);
            sqlUsuario.AddIntParam("P_ID_USUARIO", userId);
            sqlUsuario.AddVarcharParam("P_ESTADO", permissionStatus);

            return sqlUsuario;
        }

        public SqlOperations GetCreateWithReturnStatement(BaseEntity entity)
        {
            var sqlUsuario = new SqlOperations { ProcedureName = "CRE_USUARIO_WITH_RETURN_PR" };
            var newUsuario = (Usuario)entity;

            sqlUsuario.AddVarcharParam("P_DNI", newUsuario.Cedula);
            sqlUsuario.AddVarcharParam("P_NOMBRE", newUsuario.Nombre);
            sqlUsuario.AddVarcharParam("P_PRIMER_APELLIDO", newUsuario.PrimerApellido);
            sqlUsuario.AddVarcharParam("P_SEGUNDO_APELLIDO", newUsuario.SegundoApellido);
            sqlUsuario.AddVarcharParam("P_CORREO", newUsuario.Correo);
            sqlUsuario.AddVarcharParam("P_TELEFONO", newUsuario.Telefono);
            sqlUsuario.AddVarcharParam("P_ESTADO", newUsuario.Estado);

            return sqlUsuario;
        }
        public SqlOperations GetRetriveLoginStatement(string email, string password)
        {
            var sqlUsuario = new SqlOperations { ProcedureName = "RET_USUARIO_CONTRASENA_PR" };

            sqlUsuario.AddVarcharParam("P_CORREO", email);
            sqlUsuario.AddVarcharParam("P_CONTRASEÑA", password);

            return sqlUsuario;
        }
        public SqlOperations GetRetrieveByPhoneStatement(string phone)
        {
            var sqlUsuario = new SqlOperations { ProcedureName = "RET_USUARIO_BY_PHONE_PR" };

            sqlUsuario.AddVarcharParam("P_TELEFONO", phone);

            return sqlUsuario;
        }

        public SqlOperations GetRetrieveByEmailStatement(string email)
        {
            var sqlUsuario = new SqlOperations { ProcedureName = "RET_USUARIO_BY_EMAIL_PR" };

            sqlUsuario.AddVarcharParam("P_EMAIL", email);

            return sqlUsuario;
        }
        #endregion
    }
}
