using DTOs;
using NPOI.SS.Formula.Functions;
using SistemaProveeduriaDataAcccess.DAOs;
using SistemaProveeduriaDataAcccess.Mapper;

namespace SistemaProveeduriaDataAcccess.CRUD
{
    public class BitacoraCrudFactory : CrudFactory
    {
        BitacoraMapper _mapper = new BitacoraMapper();
        public BitacoraCrudFactory()
        {
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity dto)
        {
            var newBitacora = (Bitacora)dto;

            var sqlBitacora = _mapper.GetCreateStatement(newBitacora);

            dao.ExecuteProcedure(sqlBitacora);
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
            throw new NotImplementedException();
        }

        public override void Update(BaseEntity dto)
        {
            throw new NotImplementedException();
        }

        public List<T> RetrieveBitacorasByUserId<T>(int idUser)
        {
            var lstOperations = new List<T>();

            var sqlBitacora = _mapper.GetRetriveByUserIdStatement(idUser);

            var lstResults = dao.ExecuteQueryProcedure(sqlBitacora);

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
