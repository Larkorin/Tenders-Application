using DTOs;
using SistemaProveeduriaDataAcccess.DAOs;
using SistemaProveeduriaDataAcccess.Mapper;

namespace SistemaProveeduriaDataAcccess.CRUD
{
    public class PermisoCrudFactory : CrudFactory
    {
        PermisoMapper _mapper = new PermisoMapper();
        public PermisoCrudFactory()
        {
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity dto)
        {
            var newPermiso = (Permiso)dto;

            var sqlPermiso = _mapper.GetCreateStatement(newPermiso);

            dao.ExecuteProcedure(sqlPermiso);
        }

        public override void Delete(BaseEntity dto)
        {
            var permisoToDelete = (Permiso)dto;

            var sqlPermiso = _mapper.GetDeleteStatement(permisoToDelete);

            dao.ExecuteProcedure(sqlPermiso);
        }

        public override T Retrieve<T>(int id)
        {
            var sqlPermiso = _mapper.GetRetriveStatement(id);

            var lstResults = dao.ExecuteQueryProcedure(sqlPermiso);

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

            var sqlPermiso = _mapper.GetRetriveAllStatement();

            var lstResults = dao.ExecuteQueryProcedure(sqlPermiso);

            if (lstResults.Count > 0)
            {
                var objPermisos = _mapper.BuildObjects(lstResults);

                foreach (var op in objPermisos)
                {
                    lstOperations.Add((T)Convert.ChangeType(op, typeof(T)));
                }
            }

            return lstOperations;
        }

        public override void Update(BaseEntity dto)
        {
            var permisoToUpdate = (Permiso)dto;

            var sqlPermiso = _mapper.GetUpdateStatement(permisoToUpdate);

            dao.ExecuteProcedure(sqlPermiso);
        }

        public List<T> RetrieveUserPermisssions<T>(int userId)
        {
            var lstOperations = new List<T>();

            var sqlPermiso = _mapper.GetRetriveUserPermissionsStatement(userId);

            var lstResults = dao.ExecuteQueryProcedure(sqlPermiso);

            if (lstResults.Count > 0)
            {
                var objPermisos = _mapper.BuildObjectsWithStatus(lstResults);

                foreach (var op in objPermisos)
                {
                    lstOperations.Add((T)Convert.ChangeType(op, typeof(T)));
                }
            }

            return lstOperations;
        }
    }
}
