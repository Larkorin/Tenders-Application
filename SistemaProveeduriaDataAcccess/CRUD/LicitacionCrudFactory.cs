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
    public class LicitacionCrudFactory : CrudFactory
    {
        LicitacionMapper _mapper;

        public LicitacionCrudFactory()
        {
            _mapper = new LicitacionMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity dto)
        {
            var licitacion = (Licitacion)dto;

            var sqlLicitacion = _mapper.GetCreateStatement(licitacion);

            dao.ExecuteQueryProcedure(sqlLicitacion);
        }

        public override void Delete(BaseEntity dto)
        {
            var licitacion = (Licitacion)dto;

            var sqlLicitacion = _mapper.GetDeleteStatement(licitacion);

            dao.ExecuteQueryProcedure(sqlLicitacion);
        }

        public override T Retrieve<T>(int id)
        {
            var sqlLicitacion = _mapper.GetRetriveStatement(id);

            var result = dao.ExecuteQueryProcedure(sqlLicitacion);

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
            var lstLicitaciones = new List<T>();

            var sqlLicitacion = _mapper.GetRetriveAllStatement();

            //Ejecutamos el retrieve all
            var lstResults = dao.ExecuteQueryProcedure(sqlLicitacion);

            if (lstResults.Count > 0)
            {
                var objsUsers = _mapper.BuildObjects(lstResults);

                foreach (var user in objsUsers)
                {
                    lstLicitaciones.Add((T)Convert.ChangeType(user, typeof(T)));
                }
            }

            return lstLicitaciones;
        }

        public override void Update(BaseEntity dto)
        {
            var licitacion = (Licitacion)dto;

            var sqlLicitacion = _mapper.GetUpdateStatement(licitacion);

            dao.ExecuteQueryProcedure(sqlLicitacion);
        }

        public Licitacion CreateWithReturn(BaseEntity dto)
        {
            var licitacion = (Licitacion)dto;

            var sqlLicitacion = _mapper.GetCreateWithReturnStatement(licitacion);

            var lstResults = dao.ExecuteQueryProcedure(sqlLicitacion);

            if (lstResults.Count == 1)
            {
                var obj = _mapper.BuildObject(lstResults[0]);

                return (Licitacion)Convert.ChangeType(obj, typeof(Licitacion));
            }
            else
            {
                return null;
            }
        }

        public void AddProducts(int licitacionId, int productId, int quantity)
        {
            var sqlLicitacion = _mapper.GetLicitacionProductStatement(licitacionId, productId, quantity);

            dao.ExecuteQueryProcedure(sqlLicitacion);
        }

        public T RetrieveLicitacionProduct<T>(int idLicitacion, int idProducto)
        {
            var sqlLicitacion = _mapper.GetLicitacionProductByIdStatement(idLicitacion, idProducto);

            var result = dao.ExecuteQueryProcedure(sqlLicitacion);

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
        public void UpdateLicitacionProducto(int licitacionId, int productId, int quantity)
        {
            var sqlLicitacion = _mapper.GetUpdateLicitacionProductStatement(licitacionId, productId, quantity);

            dao.ExecuteProcedure(sqlLicitacion);
        }

        public List<T> GetAllProductsByLicitacion<T>(int licitacionId)
        {
            var lstLicitaciones = new List<T>();

            var sqlLicitacion = _mapper.GetAllProductsByLicitacionStatement(licitacionId);

            //Ejecutamos el retrieve all
            var lstResults = dao.ExecuteQueryProcedure(sqlLicitacion);

            if (lstResults.Count > 0)
            {
                var objsUsers = _mapper.BuildProductObjects(lstResults);

                foreach (var user in objsUsers)
                {
                    lstLicitaciones.Add((T)Convert.ChangeType(user, typeof(T)));
                }
            }

            return lstLicitaciones;
        }

        public void AdjudicarLicitacionOferta(int licitacionId, int ofertaId)
        {
            var sqlLicitacion = _mapper.GetAdjudicarLicitacionOfertaStatement(licitacionId, ofertaId);

            dao.ExecuteQueryProcedure(sqlLicitacion);
        }

    }
}