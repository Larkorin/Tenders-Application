using DTOs;
using SistemaProveeduriaDataAcccess.DAOs;

namespace SistemaProveeduriaDataAcccess.Mapper
{
    public interface ISqlStatements
    {
        SqlOperations GetCreateStatement(BaseEntity entity);
        SqlOperations GetRetriveStatement(int id);
        SqlOperations GetRetriveAllStatement();
        SqlOperations GetUpdateStatement(BaseEntity entity);
        SqlOperations GetDeleteStatement(BaseEntity entity);
    }
}