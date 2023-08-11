using DTOs;
using DTOs.ProyectoDTOs;
using NPOI.SS.Formula.Functions;
using SistemaProveeduriaDataAcccess.DAOs;
using SistemaProveeduriaDataAcccess.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaProveeduriaDataAcccess.CRUD
{
    public class MembresiaCrudFactory : CrudFactory
    {
        MembresiaMapper _mapper;

        public MembresiaCrudFactory()
        {
            _mapper = new MembresiaMapper();
            dao = SqlDao.GetInstance();
        }
        public override void Create(BaseEntity dto)
        {
            var memberShip = (Membresia)dto;
            var memberShipSql = _mapper.GetCreateStatement(memberShip);
            dao.ExecuteProcedure(memberShipSql);
        }

        public override void Delete(BaseEntity dto)
        {
            var memberShip = (Membresia)dto;
            var memberShipSql = _mapper.GetDeleteStatement(memberShip);
            dao.ExecuteProcedure(memberShipSql);
        }

        public override T Retrieve<T>(int id)
        {
            var sqlMemberShip = _mapper.GetRetriveStatement(id);
            var results = dao.ExecuteQueryProcedure(sqlMemberShip);

            if (results.Count == 1)
            {
                var objUser = _mapper.BuildObject(results[0]);

                return ((T)Convert.ChangeType(objUser, typeof(T)));
            }
            return default;
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstMemberShip = new List<T>();

            //Buscamos el statement para hacer un retrieve all
            var sqlMemberShip = _mapper.GetRetriveAllStatement();

            //Ejecutamos el retrieve all
            var lstResults = dao.ExecuteQueryProcedure(sqlMemberShip);

            if (lstResults.Count > 0)
            {
                var objsMathOperation = _mapper.BuildObjects(lstResults);

                foreach (var op in objsMathOperation)
                {
                    lstMemberShip.Add((T)Convert.ChangeType(op, typeof(T)));
                }
            }

            return lstMemberShip;
        }

        public override void Update(BaseEntity dto)
        {
            var memberShip = (Membresia)dto;
            var memberShipSql = _mapper.GetUpdateStatement(memberShip);
            dao.ExecuteProcedure(memberShipSql);
        }
        public List<T> RetrieveAllImpuestoMembresia()
        {
            var lstMemberShip = new List<T>();

            //Buscamos el statement para hacer un retrieve all
            var sqlMemberShip = _mapper.GetRetrieveAllMembresiaImpuestoStatement();

            //Ejecutamos el retrieve all
            var lstResults = dao.ExecuteQueryProcedure(sqlMemberShip);

            if (lstResults.Count > 0)
            {
                var objsMathOperation = _mapper.BuildObjects(lstResults);

                foreach (var op in objsMathOperation)
                {
                    lstMemberShip.Add((T)Convert.ChangeType(op, typeof(T)));
                }
            }

            return lstMemberShip;
        }

        public T RetrieveTaxes(int idTax, int idMembership)
        {
            var sqlMembership = _mapper.GetTaxesAlreadyAssignStatement(idTax, idMembership);

            var lstResults = dao.ExecuteQueryProcedure(sqlMembership);

            if (lstResults.Count == 1)
            {
                var obj = _mapper.BuildName(lstResults[0]);

                return (T)Convert.ChangeType(obj, typeof(T));
            }
            else
            {
                return default(T);
            }
        }

        public void UpdateImpuestoMembresia(int idImpuesto, int idMembresia)
        {
            var memberShipSql = _mapper.GetUpdateMembresiaImpuestoStatement(idImpuesto, idMembresia);
            dao.ExecuteProcedure(memberShipSql);
        }

        public void AddTaxes(int idImpuesto, int idMembresia)
        {
            var sqlMembresia = _mapper.GetCreateMembresiaImpuestoStatement(idImpuesto, idMembresia);

            dao.ExecuteProcedure(sqlMembresia);
        }
        public Membresia CreateWithReturn(BaseEntity dto)
        {
            var newMembership = (Membresia)dto;

            var sqlMembership = _mapper.GetCreateWithReturnStatement(newMembership);

            var lstResults = dao.ExecuteQueryProcedure(sqlMembership);

            if (lstResults.Count == 1)
            {
                var obj = _mapper.BuildObject(lstResults[0]);

                return (Membresia)Convert.ChangeType(obj, typeof(Membresia));
            }
            else
            {
                return null;
            }
        }
    }
}
