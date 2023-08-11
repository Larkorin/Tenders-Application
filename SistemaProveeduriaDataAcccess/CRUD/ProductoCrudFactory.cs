using DTOs;
using SistemaProveeduriaDataAcccess.DAOs;
using SistemaProveeduriaDataAcccess.Mapper;

namespace SistemaProveeduriaDataAcccess.CRUD
{
    public class ProductoCrudFactory : CrudFactory
    {
        ProductoMapper _mapper = new ProductoMapper();
        public ProductoCrudFactory()
        {
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity dto)
        {
            var newProducto = (Producto)dto;

            var sqlProducto = _mapper.GetCreateStatement(newProducto);

            dao.ExecuteProcedure(sqlProducto);
        }

        public override void Delete(BaseEntity dto)
        {
            var productoToDelete = (Producto)dto;

            var sqlProducto = _mapper.GetDeleteStatement(productoToDelete);

            dao.ExecuteProcedure(sqlProducto);
        }

        public override T Retrieve<T>(int id)
        {
            var sqlProducto = _mapper.GetRetriveStatement(id);

            var lstResults = dao.ExecuteQueryProcedure(sqlProducto);

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

            var sqlProducto = _mapper.GetRetriveAllStatement();

            var lstResults = dao.ExecuteQueryProcedure(sqlProducto);

            if (lstResults.Count > 0)
            {
                var objProductos = _mapper.BuildObjects(lstResults);

                foreach (var op in objProductos)
                {
                    lstOperations.Add((T)Convert.ChangeType(op, typeof(T)));
                }
            }

            return lstOperations;
        }

        public override void Update(BaseEntity dto)
        {
            var productoToUpdate = (Producto)dto;

            var sqlProducto = _mapper.GetUpdateStatement(productoToUpdate);

            dao.ExecuteProcedure(sqlProducto);
        }

        public Producto CreateWithReturn(BaseEntity dto)
        {
            var newUsuario = (Producto)dto;

            var sqlProducto = _mapper.GetCreateWithReturnStatement(newUsuario);

            var lstResults = dao.ExecuteQueryProcedure(sqlProducto);

            if (lstResults.Count == 1)
            {
                var obj = _mapper.BuildObject(lstResults[0]);

                return (Producto)Convert.ChangeType(obj, typeof(Producto));
            }
            else
            {
                return null;
            }
        }

        public void AssignTaxes(int taxId, int userId)
        {
            var sqlProducto = _mapper.GetAssignTaxesStatement(taxId, userId);

            dao.ExecuteProcedure(sqlProducto);
        }
    }
}
