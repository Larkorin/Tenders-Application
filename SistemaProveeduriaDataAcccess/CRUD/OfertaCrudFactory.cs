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
    public class OfertaCrudFactory : CrudFactory
    {
        OfertaMapper _mapper;
        public OfertaCrudFactory()
        {
            _mapper = new OfertaMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity dto)
        {
            var oferta = (Oferta)dto;

            var sqlOferta = _mapper.GetCreateStatement(oferta);

            dao.ExecuteQueryProcedure(sqlOferta);
        }

        public override void Delete(BaseEntity dto)
        {
            var oferta = (Oferta)dto;

            var sqlOferta = _mapper.GetDeleteStatement(oferta);

            dao.ExecuteQueryProcedure(sqlOferta);
        }

        public override T Retrieve<T>(int id)
        {
            var sqlOferta = _mapper.GetRetriveStatement(id);

            var result = dao.ExecuteQueryProcedure(sqlOferta);

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
            var lstOfertas = new List<T>();

            var sqlOferta = _mapper.GetRetriveAllStatement();

            //Ejecutamos el retrieve all
            var lstResults = dao.ExecuteQueryProcedure(sqlOferta);

            if (lstResults.Count > 0)
            {
                var objsUsers = _mapper.BuildObjects(lstResults);

                foreach (var user in objsUsers)
                {
                    lstOfertas.Add((T)Convert.ChangeType(user, typeof(T)));
                }
            }

            return lstOfertas;
        }

        public override void Update(BaseEntity dto)
        {
            var oferta = (Oferta)dto;

            var sqlOferta = _mapper.GetUpdateStatement(oferta);

            dao.ExecuteQueryProcedure(sqlOferta);
        }

        public Oferta CreateWithReturn(BaseEntity dto)
        {
            var oferta = (Oferta)dto;

            var sqlOferta = _mapper.GetCreateStatement(oferta);

            var lstResults = dao.ExecuteQueryProcedure(sqlOferta);

            if (lstResults.Count == 1)
            {
                var obj = _mapper.BuildObject(lstResults[0]);

                return (Oferta)Convert.ChangeType(obj, typeof(Oferta));
            }
            else
            {
                return null;
            }
        }

        public void AddProducts(int ofertaId, int productId, int quantity)
        {
            var sqlLicitacion = _mapper.GetOfertaProductStatement(ofertaId, productId, quantity);

            dao.ExecuteQueryProcedure(sqlLicitacion);
        }

        public List<T> RetrieveAllOffersByTenderId<T>(int id)
        {
            var lstOfertas = new List<T>();

            var sqlOferta = _mapper.GetRetriveOfferByTenderIdStatement(id);

            //Ejecutamos el retrieve all
            var lstResults = dao.ExecuteQueryProcedure(sqlOferta);

            if (lstResults.Count > 0)
            {
                var objsUsers = _mapper.BuildObjects(lstResults);

                foreach (var user in objsUsers)
                {
                    lstOfertas.Add((T)Convert.ChangeType(user, typeof(T)));
                }
            }

            return lstOfertas;
        }
        
        public List<T> RetriveOfferByUserId<T>(int id)
        {
            var lstOfertas = new List<T>();

            var sqlOferta = _mapper.GetRetriveOfferByUserIdStatement(id);

            //Ejecutamos el retrieve all
            var lstResults = dao.ExecuteQueryProcedure(sqlOferta);

            if (lstResults.Count > 0)
            {
                var objsUsers = _mapper.BuildObjects(lstResults);

                foreach (var user in objsUsers)
                {
                    lstOfertas.Add((T)Convert.ChangeType(user, typeof(T)));
                }
            }

            return lstOfertas;
        }

        public List<T> RetriveProductsByOfferId<T>(int id)
        {
            var lstProducts = new List<T>();

            var sqlOferta = _mapper.GetRetriveProductsByOfferIdStatement(id);

            //Ejecutamos el retrieve all
            var lstResults = dao.ExecuteQueryProcedure(sqlOferta);

            if (lstResults.Count > 0)
            {
                var objsUsers = _mapper.BuildProductObjects(lstResults);

                foreach (var user in objsUsers)
                {
                    lstProducts.Add((T)Convert.ChangeType(user, typeof(T)));
                }
            }

            return lstProducts;
        }
    }
}
