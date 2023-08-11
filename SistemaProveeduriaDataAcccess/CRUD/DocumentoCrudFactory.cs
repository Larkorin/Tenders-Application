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
    public class DocumentoCrudFactory : CrudFactory
    {
        DocumentoMapper _mapper;

        public DocumentoCrudFactory()
        {
            _mapper = new DocumentoMapper();
            dao = SqlDao.GetInstance();
        }
        public override void Create(BaseEntity dto)
        {
            var documento = (Documento)dto;

            var sqlDocumento = _mapper.GetCreateStatement(documento);

            dao.ExecuteQueryProcedure(sqlDocumento);
        }

        public override void Delete(BaseEntity dto)
        {
            var documento = (Documento)dto;

            var sqlDocumento = _mapper.GetDeleteStatement(documento);

            dao.ExecuteQueryProcedure(sqlDocumento);
        }

        public override T Retrieve<T>(int id)
        {
            var sqlDocumento = _mapper.GetRetriveStatement(id);

            var result = dao.ExecuteQueryProcedure(sqlDocumento);

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
            var lstDocumentos = new List<T>();

            var sqlDocumento = _mapper.GetRetriveAllStatement();

            //Ejecutamos el retrieve all
            var lstResults = dao.ExecuteQueryProcedure(sqlDocumento);

            if (lstResults.Count > 0)
            {
                var objsUsers = _mapper.BuildObjects(lstResults);

                foreach (var user in objsUsers)
                {
                    lstDocumentos.Add((T)Convert.ChangeType(user, typeof(T)));
                }
            }

            return lstDocumentos;
        }

        public override void Update(BaseEntity dto)
        {
            var documento = (Documento)dto;

            var sqlDocumento = _mapper.GetUpdateStatement(documento);

            dao.ExecuteQueryProcedure(sqlDocumento);
        }
    }
}
