using DTOs;
using NPOI.SS.Formula.Functions;
using SistemaProveeduriaDataAcccess.DAOs;
using SistemaProveeduriaDataAcccess.Mapper;

namespace SistemaProveeduriaDataAcccess.CRUD
{
    public class ContrasenaCrudFactory : CrudFactory
    {
        ContrasenaMapper _mapper = new ContrasenaMapper();
        public ContrasenaCrudFactory()
        {
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity dto)
        {
            var newContrasena = (Contrasena)dto;

            var sqlContrasena = _mapper.GetCreateStatement(newContrasena);

            dao.ExecuteProcedure(sqlContrasena);
        }

        public override void Delete(BaseEntity dto)
        {
            var contrasenaToDelete = (Usuario)dto;

            var sqlContrasena = _mapper.GetDeleteStatement(contrasenaToDelete);

            dao.ExecuteProcedure(sqlContrasena);
        }

        public override T Retrieve<T>(int id)
        {
            var sqlContrasena = _mapper.GetRetriveStatement(id);

            var lstResults = dao.ExecuteQueryProcedure(sqlContrasena);

            if (lstResults.Count == 1)
            {
                var obj = _mapper.BuildObject(lstResults[0]);

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

            var sqlContrasena = _mapper.GetRetriveAllStatement();

            var lstResults = dao.ExecuteQueryProcedure(sqlContrasena);

            if (lstResults.Count > 0)
            {
                var objContrasenas = _mapper.BuildObjects(lstResults);

                foreach (var op in objContrasenas)
                {
                    lstOperations.Add((T)Convert.ChangeType(op, typeof(T)));
                }
            }

            return lstOperations;
        }

        public override void Update(BaseEntity dto)
        {
            var contrasenaToUpdate = (Usuario)dto;

            var sqlContrasena = _mapper.GetUpdateStatement(contrasenaToUpdate);

            dao.ExecuteProcedure(sqlContrasena);
        }

        public T CheckLast5Passwords<T>(int idUsuario, string password)
        {
            var sqlContrasena = _mapper.CheckLast5PasswordsStatement(idUsuario, password);

            var lstResults = dao.ExecuteQueryProcedure(sqlContrasena);

            if (lstResults.Count == 1)
            {
                var obj = _mapper.BuildObject(lstResults[0]);

                return (T)Convert.ChangeType(obj, typeof(T));
            }
            else
            {
                return default(T);
            }
        }

        public void SetNewPassword(int idUsuario, string password)
        {
            var sqlContrasena = _mapper.SetNewPasswordStatement(idUsuario, password);

            dao.ExecuteProcedure(sqlContrasena);
        }
    }
}
