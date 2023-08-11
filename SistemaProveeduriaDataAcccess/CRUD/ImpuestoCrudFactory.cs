using DTOs.ProyectoDTOs;
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
    public class ImpuestoCrudFactory : CrudFactory
    {
        ImpuestoMapper _mapper;

        public ImpuestoCrudFactory()
        {
            _mapper = new ImpuestoMapper();
            dao = SqlDao.GetInstance();
        }
        public override void Create(BaseEntity dto)
        {
            var tax = (Impuesto)dto;
            var taxSql = _mapper.GetCreateStatement(tax);
            dao.ExecuteProcedure(taxSql);
        }

        public override void Delete(BaseEntity dto)
        {
            var tax = (Impuesto)dto;
            var taxSql = _mapper.GetDeleteStatement(tax);
            dao.ExecuteProcedure(taxSql);
        }

        public override T Retrieve<T>(int id)
        {
            var sqlTaxes = _mapper.GetRetriveStatement(id);
            var results = dao.ExecuteQueryProcedure(sqlTaxes);

            if (results.Count == 1)
            {
                var objUser = _mapper.BuildObject(results[0]);

                return ((T)Convert.ChangeType(objUser, typeof(T)));

            }
            return default;
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstTaxes = new List<T>();

            //Buscamos el statement para hacer un retrieve all
            var sqlTax = _mapper.GetRetriveAllStatement();

            //Ejecutamos el retrieve all
            var lstResults = dao.ExecuteQueryProcedure(sqlTax);

            if (lstResults.Count > 0)
            {
                var objsMathOperation = _mapper.BuildObjects(lstResults);

                foreach (var op in objsMathOperation)
                {
                    lstTaxes.Add((T)Convert.ChangeType(op, typeof(T)));
                }
            }

            return lstTaxes;
        }

        public override void Update(BaseEntity dto)
        {
            var tax = (Impuesto)dto;
            var taxSql = _mapper.GetUpdateStatement(tax);
            dao.ExecuteProcedure(taxSql);
        }
        public List<T> RetrieveMembershipTaxes<T>(int idMembership)
        {
            var lstOperations = new List<T>();

            var sqlTax = _mapper.GetRetriveMemberShipTaxStatement(idMembership);

            var lstResults = dao.ExecuteQueryProcedure(sqlTax);

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
    }
}
