using DTOs;
using NPOI.SS.Formula.Functions;
using SistemaProveeduriaDataAcccess.DAOs;
using SistemaProveeduriaDataAcccess.Mapper;

namespace SistemaProveeduriaDataAcccess.CRUD
{
    public class FacturaCrudFactory : CrudFactory
    {
        FacturaMapper _mapper = new FacturaMapper();
        public FacturaCrudFactory()
        {
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity dto)
        {
            var newFactura = (Factura)dto;

            var sqlFactura = _mapper.GetCreateStatement(newFactura);

            dao.ExecuteProcedure(sqlFactura);
        }

        public override void Delete(BaseEntity dto)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>(int id)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstBills = new List<T>();

            //Buscamos el statement para hacer un retrieve all
            var sqlTax = _mapper.GetRetriveAllStatement();

            //Ejecutamos el retrieve all
            var lstResults = dao.ExecuteQueryProcedure(sqlTax);

            if (lstResults.Count > 0)
            {
                var objsMathOperation = _mapper.BuildObjects(lstResults);

                foreach (var op in objsMathOperation)
                {
                    lstBills.Add((T)Convert.ChangeType(op, typeof(T)));
                }
            }

            return lstBills;
        }

        public override void Update(BaseEntity dto)
        {
            throw new NotImplementedException();
        }

        public List<T> RetriveByUserInCharge<T>(int idUser)
        {
            var lstOperations = new List<T>();

            var sqlFactura = _mapper.GetRetriveByUserInChargeStatement(idUser);

            var lstResults = dao.ExecuteQueryProcedure(sqlFactura);

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
        
        public List<T> RetriveByUserReceiver<T>(int idUser)
        {
            var lstOperations = new List<T>();

            var sqlFactura = _mapper.GetRetriveByUserReceiverStatement(idUser);

            var lstResults = dao.ExecuteQueryProcedure(sqlFactura);

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
