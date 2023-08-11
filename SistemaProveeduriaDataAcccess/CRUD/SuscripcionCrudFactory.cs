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
    public class SuscripcionCrudFactory : CrudFactory
    {
        SuscripcionMapper _mapper;

        public SuscripcionCrudFactory()
        {
            _mapper = new SuscripcionMapper();
            dao = SqlDao.GetInstance();
        }
        public override void Create(BaseEntity dto)
        {
            var subs = (Suscripcion)dto;
            var subsSql = _mapper.GetCreateStatement(subs);
            dao.ExecuteProcedure(subsSql);
        }

        public override void Delete(BaseEntity dto)
        {
            var subs = (Suscripcion)dto;
            var subsSql = _mapper.GetDeleteStatement(subs);
            dao.ExecuteProcedure(subsSql);
        }

        public override T Retrieve<T>(int id)
        {
            var sqlSub = _mapper.GetRetriveStatement(id);
            var results = dao.ExecuteQueryProcedure(sqlSub);

            if (results.Count == 1)
            {
                var objUser = _mapper.BuildObject(results[0]);

                return ((T)Convert.ChangeType(objUser, typeof(T)));

            }
            return default;
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstSubs = new List<T>();

            //Buscamos el statement para hacer un retrieve all
            var sqlSub = _mapper.GetRetriveAllStatement();

            //Ejecutamos el retrieve all
            var lstResults = dao.ExecuteQueryProcedure(sqlSub);

            if (lstResults.Count > 0)
            {
                var objsMathOperation = _mapper.BuildObjects(lstResults);

                foreach (var op in objsMathOperation)
                {
                    lstSubs.Add((T)Convert.ChangeType(op, typeof(T)));
                }
            }

            return lstSubs;
        }

        public override void Update(BaseEntity dto)
        {
            var subs = (Suscripcion)dto;
            var subsSql = _mapper.GetUpdateStatement(subs);
            dao.ExecuteProcedure(subsSql);
        }
    }
}
