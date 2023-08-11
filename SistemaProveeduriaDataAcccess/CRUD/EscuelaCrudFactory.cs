using DTOs;
using SistemaProveeduriaDataAcccess.DAOs;
using SistemaProveeduriaDataAcccess.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaProveeduriaDataAcccess.CRUD
{
    public class EscuelaCrudFactory : CrudFactory
    {
        EscuelaMapper _mapper;

        public EscuelaCrudFactory()
        {
            _mapper = new EscuelaMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity dto)
        {
            var escuela = (Escuela)dto;

            var sqlEscuela = _mapper.GetCreateStatement(escuela);

            dao.ExecuteQueryProcedure(sqlEscuela);
        }

        public override void Delete(BaseEntity dto)
        {
            var escuela = (Escuela)dto;

            var sqlEscuela = _mapper.GetDeleteStatement(escuela);

            dao.ExecuteQueryProcedure(sqlEscuela);
        }

        public override T Retrieve<T>(int id)
        {
            var sqlEscuela = _mapper.GetRetriveStatement(id);

            var result = dao.ExecuteQueryProcedure(sqlEscuela);

            if (result.Count == 1)
            {
                var obj = _mapper.BuildObject(result[0]);
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            else
            {
                return default;
            }
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstEscuelas = new List<T>();

            var sqlEscuela = _mapper.GetRetriveAllStatement();

            //Ejecutamos el retrieve all
            var lstResults = dao.ExecuteQueryProcedure(sqlEscuela);

            if (lstResults.Count > 0)
            {
                var objsUsers = _mapper.BuildObjects(lstResults);

                foreach (var user in objsUsers)
                {
                    lstEscuelas.Add((T)Convert.ChangeType(user, typeof(T)));
                }
            }

            return lstEscuelas;
        }

        public override void Update(BaseEntity dto)
        {
            var escuela = (Escuela)dto;

            var sqlEscuela = _mapper.GetUpdateStatement(escuela);

            dao.ExecuteQueryProcedure(sqlEscuela);
        }
    }
}
