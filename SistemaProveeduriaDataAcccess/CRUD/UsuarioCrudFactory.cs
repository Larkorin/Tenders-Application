using DTOs;
using SistemaProveeduriaDataAcccess.DAOs;
using SistemaProveeduriaDataAcccess.Mapper;

namespace SistemaProveeduriaDataAcccess.CRUD
{
    public class UsuarioCrudFactory : CrudFactory
    {
        UsuarioMapper _userMapper = new UsuarioMapper();
        PermisoMapper _permissionMapper = new PermisoMapper();
        public UsuarioCrudFactory()
        {
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity dto)
        {
            var newUsuario = (Usuario)dto;

            var sqlUsuario = _userMapper.GetCreateStatement(newUsuario);

            dao.ExecuteProcedure(sqlUsuario);
        }

        public override void Delete(BaseEntity dto)
        {
            var usuarioToDelete = (Usuario)dto;

            var sqlUsuario = _userMapper.GetDeleteStatement(usuarioToDelete);

            dao.ExecuteProcedure(sqlUsuario);
        }

        public override T Retrieve<T>(int id)
        {
            var sqlUsuario = _userMapper.GetRetriveStatement(id);

            var lstResults = dao.ExecuteQueryProcedure(sqlUsuario);

            if (lstResults.Count == 1)
            {
                var obj = _userMapper.BuildObject(lstResults[0]);

                return (T)Convert.ChangeType(obj, typeof(T));
            }
            else
            {
                return default(T);
            }
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstOperations = new List<T>();

            var sqlUsuario = _userMapper.GetRetriveAllStatement();

            var lstResults = dao.ExecuteQueryProcedure(sqlUsuario);

            if (lstResults.Count > 0)
            {
                var objUsuarios = _userMapper.BuildObjects(lstResults);

                foreach (var op in objUsuarios)
                {
                    lstOperations.Add((T)Convert.ChangeType(op, typeof(T)));
                }
            }

            return lstOperations;
        }

        public override void Update(BaseEntity dto)
        {
            var userToUpdate = (Usuario)dto;

            var sqlUsuario = _userMapper.GetUpdateStatement(userToUpdate);

            dao.ExecuteProcedure(sqlUsuario);
        }

        public void AssignPermissions(int permissionId, int userId)
        {
            var sqlUsuario = _userMapper.GetAssignPermissionsStatement(permissionId, userId);

            dao.ExecuteProcedure(sqlUsuario);
        }

        public void UpdateUserPermissions(int permissionId, int userId, String permissionStatus)
        {
            var sqlUsuario = _userMapper.GetUpdateUserPermissionsStatement(permissionId, userId, permissionStatus);

            dao.ExecuteProcedure(sqlUsuario);
        }

        public T RetrievePermissions<T>(int permissionId, int userId)
        {
            var sqlUsuario = _userMapper.GetPermissionsAlreadyAssignStatement(permissionId, userId);

            var lstResults = dao.ExecuteQueryProcedure(sqlUsuario);

            if (lstResults.Count == 1)
            {
                var obj = _userMapper.BuildPermissionStatus(lstResults[0]);

                return (T)Convert.ChangeType(obj, typeof(T));
            }
            else
            {
                return default(T);
            }
        }

        public Usuario CreateWithReturn(BaseEntity dto)
        {
            var newUsuario = (Usuario)dto;

            var sqlUsuario = _userMapper.GetCreateWithReturnStatement(newUsuario);

            var lstResults = dao.ExecuteQueryProcedure(sqlUsuario);

            if (lstResults.Count == 1)
            {
                var obj = _userMapper.BuildObject(lstResults[0]);

                return (Usuario)Convert.ChangeType(obj, typeof(Usuario));
            }
            else
            {
                return null;
            }
        }
        public T RetrieveLogin<T>(string email, string password)
        {
            var sqlUsuario = _userMapper.GetRetriveLoginStatement(email, password);

            var lstResults = dao.ExecuteQueryProcedure(sqlUsuario);

            if (lstResults.Count == 1)
            {
                var obj = _userMapper.BuildObject(lstResults[0]);

                return (T)Convert.ChangeType(obj, typeof(T));
            }
            else
            {
                return default(T);
            }
        }
        public T RetrievePhone<T>(string phone)
        {
            var sqlUsuario = _userMapper.GetRetrieveByPhoneStatement(phone);

            var lstResults = dao.ExecuteQueryProcedure(sqlUsuario);

            if (lstResults.Count == 1)
            {
                var obj = _userMapper.BuildObject(lstResults[0]);

                return (T)Convert.ChangeType(obj, typeof(T));
            }
            else
            {
                return default(T);
            }
        }
        public T RetrieveEmail<T>(string email)
        {
            var sqlUsuario = _userMapper.GetRetrieveByEmailStatement(email);

            var lstResults = dao.ExecuteQueryProcedure(sqlUsuario);

            if (lstResults.Count == 1)
            {
                var obj = _userMapper.BuildObject(lstResults[0]);

                return (T)Convert.ChangeType(obj, typeof(T));
            }
            else
            {
                return default(T);
            }
        }
    }
}
